
using System.Text.RegularExpressions;

namespace VozAmiga.Api.Utils;


public static partial class StringExtensions
{
    private static readonly Regex _selectRedundantWhiteSpace = _CreateRemoveRedundantSpaceRegex();
    private static readonly Regex _removeNonLetters = _RemoveNonLetters();
    private static readonly Regex _onlyDigits = _OnlyDigits();
    /// <summary>
    /// Replace tow or more reduntand white space caracters with a single space
    /// </summary>
    /// <param name="value"></param>
    /// <returns>A <see cref="string"/> with no redundant spaces</returns>
    public static string NoReduntantSpace(this string value)
    {
        // replace 2+ white space ca
        return _selectRedundantWhiteSpace.Replace(value.Trim(), " ");
    }
    /// <summary>
    /// Remove all non alphabetical chacateres
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string RemoveNonLetters(this string value)
    {
        return _removeNonLetters.Replace(value, "");
    }

    /// <summary>
    /// Remove all characteres that are not numbers
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string OnlyDigits(this string value)
    {
        return _onlyDigits.Replace(value, "");
    }

    [GeneratedRegex(@"\s+", RegexOptions.IgnoreCase | RegexOptions.Compiled, "pt-BR")]
    private static partial Regex _CreateRemoveRedundantSpaceRegex();

    [GeneratedRegex(@"[^a-z]", RegexOptions.IgnoreCase | RegexOptions.Compiled, "pt-BR")]
    private static partial Regex _RemoveNonLetters();

    [GeneratedRegex(@"\D", RegexOptions.IgnoreCase | RegexOptions.Compiled, "pt-BR")]
    private static partial Regex _OnlyDigits();
}
