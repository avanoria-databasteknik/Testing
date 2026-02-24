using Domain.Common.ValueObjects;

namespace Tests.Unit.ValueObjects;

public class EmailAddressTests
{
    [Fact]
    public void Ctor_WithValidEmail_NormalizesToLowerAndTrim()
    {
        var email = new EmailAddress("  Hans.Mattin-Lassei@Avanoria.SE  ");

        Assert.Equal("hans.mattin-lassei@avanoria.se", email.Value);
        Assert.Equal("hans.mattin-lassei@avanoria.se", email.ToString());
    }

    [Theory]
    [InlineData("a@b.com")]
    [InlineData("john.doe+tag@sub.example.co.uk")]
    [InlineData("x_y-z@domain.io")]
    public void Ctor_WithValidEmail_SetsValue(string input)
    {
        var email = new EmailAddress(input);

        Assert.Equal(input.Trim().ToLowerInvariant(), email.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Ctor_WithNullOrWhitespace_ThrowsArgumentException(string? input)
    {
        var ex = Assert.Throws<ArgumentException>(() => new EmailAddress(input!));

        Assert.Contains("cannot be empty", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("plainaddress")]
    [InlineData("missingatsign.com")]
    [InlineData("missingdomain@")]
    [InlineData("@missinglocalpart.com")]
    [InlineData("a@b")]
    [InlineData("a@b.")]
    [InlineData("a@.com")]
    [InlineData("a b@c.com")]
    public void Ctor_WithInvalidFormat_ThrowsArgumentException(string input)
    {
        var ex = Assert.Throws<ArgumentException>(() => new EmailAddress(input));

        Assert.Contains("invalid email address format", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Equality_SameEmailDifferentCasingOrWhitespace_AreEqual()
    {
        var a = new EmailAddress("  Test@Example.com ");
        var b = new EmailAddress("test@example.com");

        Assert.Equal(a, b);
        Assert.True(a == b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }
}
