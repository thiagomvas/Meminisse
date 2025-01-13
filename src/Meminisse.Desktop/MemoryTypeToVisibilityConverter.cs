using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Meminisse.Core.ValueTypes;

namespace Meminisse.Desktop;

public class MemoryTypeToVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is MemoryType type && parameter is string paramString)
        {
            if (type is MemoryType.Image && paramString == "image")
            {
                return true;
            }
            else if (type is MemoryType.Text && paramString == "text")
            {
                return true;
            }
        }

        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}