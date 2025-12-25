using System.Collections.ObjectModel;

namespace TriviaGame;

/// <summary>
/// Runtime authority for a single round.
/// Validates commands, enforces rules, and emits events.
/// </summary>
public sealed class RoundEngine(GameRound session, GameSessionRules rules)
{
    public GameRound Session { get; private set; } = session ?? throw new ArgumentNullException(nameof(session));
    private readonly GameSessionRules _rules = rules ?? throw new ArgumentNullException(nameof(rules));

    public IEnumerable<IGameEvent> SubmitAnswer(Player player, string value, DateTimeOffset submittedAtUtc)
    {
        if (submittedAtUtc.Offset != TimeSpan.Zero)
            throw new ArgumentException("submittedAtUtc must be UTC.", nameof(submittedAtUtc));

        // Authority rules
        if (Session.Phase != RoundPhase.Answering)
            yield return new CommandRejected("Not in Answering phase.");
        else if (submittedAtUtc > Session.AnswerDeadlineUtc)
            yield return new CommandRejected("Answer deadline passed.");
        else if (!Session.EligiblePlayers.Contains(player))
            yield return new CommandRejected("Player not eligible for this round.");
        else
        {
            // If you later support multi-attempts, this becomes rule-dependent.
            var answer = new Answer(player, submittedAtUtc, value);
            yield return new AnswerSubmitted(Session.RoundId, answer);
        }
    }

    public IEnumerable<IGameEvent> Tick(DateTimeOffset nowUtc)
    {
        if (nowUtc.Offset != TimeSpan.Zero)
            throw new ArgumentException("nowUtc must be UTC.", nameof(nowUtc));

        // Authoritative close of answering
        if (Session.Phase == RoundPhase.Answering && nowUtc >= Session.AnswerDeadlineUtc)
        {
            yield return new AnsweringClosed(Session.RoundId, Session.AnswerDeadlineUtc);

            // Reveal auto-start can be policy-driven; here's one sane default:
            // - if RevealMode.Deferred: move to Reveal
            // - if Immediate: you might reveal per-answer elsewhere
            if (_rules.RevealMode == RevealMode.Deferred)
                yield return new RevealStarted(Session.RoundId, nowUtc);
            else
                yield return new IntermissionStarted(Session.RoundId, nowUtc);
        }

        if (Session.Phase == RoundPhase.Reveal &&
            _rules.RevealControlMode == PhaseControlMode.Authoritative &&
            Session.RevealEndsAtUtc is { } revealEnds &&
            nowUtc >= revealEnds)
        {
            yield return new RevealEnded(Session.RoundId, revealEnds);
            yield return new IntermissionStarted(Session.RoundId, nowUtc);
        }

        if (Session.Phase == RoundPhase.Intermission &&
            _rules.AdvanceControlMode == PhaseControlMode.Authoritative &&
            Session.IntermissionEndsAtUtc is { } intermissionEnds &&
            nowUtc >= intermissionEnds)
        {
            yield return new IntermissionEnded(Session.RoundId, intermissionEnds);
            yield return new RoundCompleted(Session.RoundId, nowUtc);
        }
    }

    /// <summary>
    /// Apply events to the persisted session state (pure-ish reducer hosted here for convenience).
    /// </summary>
    public void Apply(IGameEvent @event)
    {
        switch (@event)
        {
            case AnswerSubmitted e:
                {
                    var next = new Dictionary<PlayerId, Answer>(Session.AnswersByPlayer)
                    {
                        [e.Answer.Player.Id] = e.Answer
                    };
                    Session = Session with
                    {
                        AnswersByPlayer = new ReadOnlyDictionary<PlayerId, Answer>(next)
                    };
                    break;
                }
            case RevealStarted:
                Session = Session with { Phase = RoundPhase.Reveal };
                break;

            case IntermissionStarted:
                Session = Session with { Phase = RoundPhase.Intermission };
                break;

            case RoundCompleted:
                Session = Session with { Phase = RoundPhase.Completed };
                break;

            case AnsweringClosed:
                // No state change required if you derive “closed” from time/phase,
                // but keeping the event is useful for logs/replay.
                break;

            // CommandRejected typically does not mutate state
            case CommandRejected:
                break;

            default:
                throw new NotSupportedException($"Unhandled event type: {@event.GetType().Name}");
        }
    }
}
