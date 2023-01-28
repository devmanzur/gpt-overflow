using System.ComponentModel;
using System.Text;

namespace GPTOverflow.Core.CrossCuttingConcerns.Utils;

public static class EnumUtils
{
    public static bool BelongToType<T>(string text) where T : Enum
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            return false;

        var list = Enum.GetValues(typeof(T)) as T[];
        return list?.Any(x => x.ToString() == text) ?? false;
    }


    public static T ToEnum<T>(this string text) where T : Enum
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            throw new ArgumentNullException(nameof(T), "invalid value for enum");

        var list = Enum.GetValues(typeof(T)) as T[];

        return list!.FirstOrDefault(x => x.ToString() == text) ??
               throw new ArgumentOutOfRangeException(nameof(T), "invalid value for enum");
    }

    public static string Description<T>(this T e) where T : Enum
    {
        var info = e.GetType().GetField(e.ToString());
        if (info == null) return e.ToString();
        var attributes = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes[0].Description;
    }

    public static T[] ToList<T>()
    {
        return (T[])Enum.GetValues(typeof(T));
    }

    public static string ToSpacedSentence<T>(this T e, bool preserveAcronyms = false) where T : Enum
    {
        var text = e.ToString();
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;
        var newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (var i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]))
                if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                    (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                     i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                    newText.Append(' ');
            newText.Append(text[i]);
        }

        return newText.ToString();
    }
}