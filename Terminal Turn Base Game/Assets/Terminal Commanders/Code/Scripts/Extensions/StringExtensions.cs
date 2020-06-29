using System;
using System.Linq;

public static class StringExtensions
{
    private const string Empty = "";

    public static string FirstCharToUpper(this string input)
    {
        switch (input)
        {
            case null: throw new ArgumentNullException(nameof(input));
            case Empty: throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
            default: return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }

    public static string FirstCharToLower(this string input)
    {
        switch (input)
        {
            case null: throw new ArgumentNullException(nameof(input));
            case Empty: throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
            default: return input.First().ToString().ToLower() + input.Substring(1);
        }
    }

    public static string RemoveAllWhiteSpace(this string input)
    {
        switch (input)
        {
            case null: throw new ArgumentNullException(nameof(input));
            case Empty: throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
            default: return new string (input.ToCharArray().Where(x => !char.IsWhiteSpace(x)).ToArray());
        }
    }

    public static char FirstLetterToChar(this string input, bool useSpace = false)
    {
        switch (input)
        {
            case null: throw new ArgumentNullException(nameof(input));
            case Empty: throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
            default: return input[0] == ' ' && useSpace ? input[0] : input.TrimStart()[0];
        }
    }

    public static bool Contains(this string[] stringArray, string input)
    {
        switch (stringArray)
        {
            case null: throw new ArgumentNullException(nameof(stringArray));
            default:
                foreach (string s in stringArray)
                {
                    if (s == input)
                        return true;
                }

                return false;
        }
    }
}
