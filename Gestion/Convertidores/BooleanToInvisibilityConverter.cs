using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Gestion.Convertidores {
    public class BooleanToInvisibilityConverter : IValueConverter {
        #region Miembros de IValueConverter

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value is bool)
                if ((bool)value)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            else if (value is bool?)
                if (((bool?)value) == true)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            throw new InvalidOperationException("El tipo debe ser booleano.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (Visibility)value == Visibility.Collapsed;
        }

        #endregion
    }
}
