using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos {
    public class Cliente: EntityBase {
        private int id = 0;
        public int Id {
            get { return id; }
            set {
                if (id != value) {
                    id = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string razonSocial;
        public string RazonSocial {
            get { return razonSocial; }
            set {
                if (razonSocial != value) {
                    razonSocial = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public override string this[string columnName] {
            get {
                switch (columnName) {
                    case "RazonSocial": 
                        if(string.IsNullOrWhiteSpace(RazonSocial)) {
                            return "La Razon Social es obligatoria.";
                        }
                        break;
                }
                return base[columnName];
            }
        }
    }
}
