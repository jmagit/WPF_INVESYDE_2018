using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core {
    public abstract class ObservableBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null) {
            Debug.Assert(
                string.IsNullOrEmpty(propertyName) ||
                (GetType().GetProperty(propertyName) != null),
                "Check that the property name exists for this instance.");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class EntityBase : ObservableBase, IDataErrorInfo, INotifyDataErrorInfo {
        public virtual string this[string columnName] => null;

        public virtual string Error {
            get {
                var rslt = "";
                foreach (var p in GetType().GetProperties())
                    if (!String.IsNullOrEmpty(this[p.Name]))
                        rslt += "\n" + this[p.Name];
                return rslt == "" ? null : rslt.Substring(1);
            }
        }

        public bool HasErrors => Error != null;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        protected void NotifyErrorsChanged(string nombre) {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nombre));
        }
        protected override void NotifyPropertyChanged(string nombre) {
            base.NotifyPropertyChanged(nombre);
            NotifyErrorsChanged(nombre);
        }
        public IEnumerable GetErrors(string propertyName) {
            return new string[] { this[propertyName] };
        }

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName) {
            return new string[] { this[propertyName] };
        }
    }
}
