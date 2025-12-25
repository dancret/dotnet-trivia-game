namespace TriviaGame;

public static class Helpers
{
    public static string Id => Guid.NewGuid().ToString("N");
}