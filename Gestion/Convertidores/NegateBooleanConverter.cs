using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Gestion.Convertidores {
    public class NegateBooleanConverter : IValueConverter {
        #region Miembros de IValueConverter

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value is Boolean)
                return !(bool)value;
            else
                throw new InvalidOperationException("El tipo debe ser Boolean.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value is Boolean)
                return !(bool)value;
            else
                throw new InvalidOperationException("El tipo debe ser Boolean.");
        }

        #endregion
    }
}
