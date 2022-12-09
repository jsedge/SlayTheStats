using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace SlayTheStats.Converters;

public class CharacterConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture){            
        var ti = culture.TextInfo;
        var text = (string)value;
        var pretty = ti.ToTitleCase(text.Replace("_", " ").ToLower());
        return pretty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var text = (string)value;
        return text.ToUpper().Replace(" ", "_");
    }

}