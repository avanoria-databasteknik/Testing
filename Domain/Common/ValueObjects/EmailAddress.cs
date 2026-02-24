using System.Text.RegularExpressions;

namespace Domain.Common.ValueObjects;

public sealed record EmailAddress(string Value)
{
    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public string Value { get; } = Normalize(Value);

    private static string Normalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email address cannot be empty.", nameof(value));

        var normalized = value.Trim().ToLowerInvariant();

        if (!EmailRegex.IsMatch(normalized))
            throw new ArgumentException("Invalid email address format.", nameof(value));

        return normalized;
    }

    public override string ToString() => Value;
}
