using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Gestion.Convertidores {
    /// <summary>
    /// Convierte a nulo las cadenas vacias
    /// </summary>
    public class StringToNullConverter : IValueConverter {
        #region Miembros de IValueConverter

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return null;
            return value;
        }

        #endregion
    }
}
