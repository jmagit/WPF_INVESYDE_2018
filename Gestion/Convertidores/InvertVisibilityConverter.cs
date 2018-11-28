using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Gestion.Convertidores {
    public class InvertVisibilityConverter : IValueConverter {
        #region Miembros de IValueConverter

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value is Visibility)
                if ((Visibility)value == Visibility.Visible)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            else
                throw new InvalidOperationException("El tipo debe ser System.Windows.Visibility.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
