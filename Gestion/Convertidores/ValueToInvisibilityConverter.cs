using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Gestion.Convertidores {
    public class ValueToInvisibilityConverter : IValueConverter {
        #region Miembros de IValueConverter

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null || parameter == null)
                return Visibility.Visible;
            else
                return value.ToString().Contains(parameter.ToString()) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }

        #endregion
    }
}
