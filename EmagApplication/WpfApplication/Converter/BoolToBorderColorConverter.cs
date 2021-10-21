using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace WpfApplication.Converter
{
    public class BoolToBorderColorConverter :
        MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        #region "IValueConverter Members"

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var empty = value != null && (bool)value;
            if (!empty)
            {
                return Brushes.Red;
            }
            return (SolidColorBrush)new BrushConverter().ConvertFrom("#4557de");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion

    }
}
