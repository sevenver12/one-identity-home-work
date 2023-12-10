using System.Text.RegularExpressions;

namespace OneIdentity.Homework.Api.Validation;

public static partial class Regexes
{
    public static readonly Regex PhoneRegex = GetPhoneRegex();

    [GeneratedRegex("""^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$""")]
    private static partial Regex GetPhoneRegex();
}
