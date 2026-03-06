using System.Text.RegularExpressions;

namespace Contoso.StorageMachine;

/// <summary>Provides reusable combinators for basic data validation.</summary>
public static class Validation
{
    /// <summary>Validate that the given string is not empty or indicate that the value is invalid by returning the provided message.</summary>
    public static Result<string, string> NonEmpty(string invalid, string s)
        => string.IsNullOrWhiteSpace(s)
            ? Result<string, string>.Error(invalid)
            : Result<string, string>.Ok(s);

    /// <summary>
    /// Validate that the given string contains only alphanumeric characters or indicate otherwise
    /// by returning the provided message.
    /// </summary>
    public static Result<string, string> AlphaNumeric(string invalid, string s)
        => s.All(char.IsLetterOrDigit)
            ? Result<string, string>.Ok(s)
            : Result<string, string>.Error(invalid);

    /// <summary>
    /// Validate that the given string matches the provided regular expression or indicate otherwise
    /// by returning the provided message.
    /// </summary>
    public static Result<string, string> Matches(Regex re, string invalid, string s)
        => re.IsMatch(s)
            ? Result<string, string>.Ok(s)
            : Result<string, string>.Error(invalid);
}
