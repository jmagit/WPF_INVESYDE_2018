using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Core {
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
        public virtual string this[string columnName] {
            get {
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, new ValidationContext(this, null, null), validationResults, true);
                if (validationResults.Count == 0) return null;
                var rslt = validationResults.FirstOrDefault(o => o.MemberNames.Any(v => v == columnName));
                if (rslt == null)
                    return null;
                else
                    return rslt.ErrorMessage;
            }
        }

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
        protected override void NotifyPropertyChanged([CallerMemberName] string propertyName = null) {
            base.NotifyPropertyChanged(propertyName);
            NotifyErrorsChanged(propertyName);
            base.NotifyPropertyChanged(nameof(HasErrors));
        }
        public IEnumerable GetErrors(string propertyName) {
            return new string[] { this[propertyName] };
        }

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName) {
            return new string[] { this[propertyName] };
        }
    }
}
