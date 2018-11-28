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
    public class CeroToInvisibilityConverter : IValueConverter {
        #region Miembros de IValueConverter

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value is Nullable || value.ToString() == "0" /* || value.ToString() == "1"*/)
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
