using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DwLang.Editor
{
    public class CollectionSizeVisbilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((IEnumerable)value)?.GetEnumerator().MoveNext() ?? false ? Visibility.Visible : Visibility.Hidden;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
