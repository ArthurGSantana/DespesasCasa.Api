using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DespesasCasa.Domain.Extensions;

public static class StringExtensions
{
    public static string? ToSnakeCase(this string? text)
    {
        if (text is null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (text.Length < 2)
        {
            return text;
        }

        var sb = new StringBuilder();
        sb.Append(char.ToLowerInvariant(text[0]));
        for (int i = 1; i < text.Length; ++i)
        {
            char c = text[i];
            if (char.IsUpper(c))
            {
                sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }

    public static string? TrimAll(this string? text)
    {
        if (text is null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        return Regex.Replace(text, @"\s+", "_", RegexOptions.None, TimeSpan.FromSeconds(5));
    }

    public static string RemoveDiacritics(this string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

        for (int i = 0; i < normalizedString.Length; i++)
        {
            char c = normalizedString[i];
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder
            .ToString()
            .Normalize(NormalizationForm.FormC);
    }

    public static string TrimMultipleWhitespaces(this string text)
    {
        RegexOptions options = RegexOptions.None;
        Regex regex = new Regex("[ ]{2,}", options, TimeSpan.FromMilliseconds(1000));
        text = regex.Replace(text, " ");
        return text.Trim();
    }
}
