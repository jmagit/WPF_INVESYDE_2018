using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using System.Collections.ObjectModel;
using Datos;

namespace MVVM.VM {
    public class ClienteVM: ObservableBase {
        private ClienteRepository db = new ClienteRepository();

        private ObservableCollection<Cliente> listado;
        public ObservableCollection<Cliente> Listado {
            get {
                return listado;
            }
        }
        private Cliente elemento;
        public Cliente Elemento {
            get {
                return elemento;
            }
            set {
                if (elemento != value) {
                    elemento = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DelegateCommand List {
            get {
                return new DelegateCommand(cmd => {
                    listado = new ObservableCollection<Cliente>(db.GetAll());
                    NotifyPropertyChanged(nameof(Listado));
                });
            }
        }

        public DelegateCommand View {
            get {
                return new DelegateCommand(cmd => {
                    Elemento = db.GetById((int)cmd);
                });
            }
        }
        public DelegateCommand Add {
            get {
                return new DelegateCommand(cmd => {
                    Elemento = new Cliente();
                });
            }
        }



    }
}
