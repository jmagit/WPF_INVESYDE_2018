using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos {
    public class Cliente: EntityBase {
        private int id;
        public int Id {
            get { return id; }
            set {
                if (id != value) {
                    id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }
        private string razonSocial;
        public string RazonSocial {
            get { return razonSocial; }
            set {
                if (razonSocial != value) {
                    razonSocial = value;
                    NotifyPropertyChanged("RazonSocial");
                    if(this["RazonSocial"] != null) {
                        NotifyErrorsChanged("RazonSocial");
                    }
                }
            }
        }
        public override string this[string columnName] {
            get {
                switch (columnName) {
                    case "RazonSocial": 
                        if(string.IsNullOrWhiteSpace(RazonSocial)) {
                            return "La Razon Social es obligatorio";
                        }
                        break;
                }
                return base[columnName];
            }
        }
    }
}
