namespace SakilaAPI.Helpers;

public class StringHelpers
{
    public static string Capitalize(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;
        input = input.ToLowerInvariant();
        return char.ToUpperInvariant(input[0]) 
            + input[1..];
    }
}