using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Gestion.Convertidores {
    /// <summary>
    /// Permite ocultar controles sin datos
    /// </summary>
    public class NullToInvisibilityConverter : IValueConverter {
        #region Miembros de IValueConverter

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
