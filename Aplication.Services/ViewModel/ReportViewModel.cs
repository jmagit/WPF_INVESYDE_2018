using Aplication.Core;
using Domain.MainModule.Entities;
using Infrastructure.CrossCutting.Core;
using Infrastructure.Data.Contracts;
using Infrastructure.Data.MainModule.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Aplication.Services.ViewModel {
    public class ReportViewModel : ObservableBase, IClosed, ICanClosed {
        private IReportRepository db = new ReportRepository();

        public event EventHandler<CancelEventArgs> Closed;

        protected virtual void OnClosed() {
            Closed?.Invoke(this, new CancelEventArgs());
        }
        protected virtual void OnClosed(ref CancelEventArgs e) {
            Closed?.Invoke(this, e);
        }

        public bool CanClosed() {
            return modo != EstadoCRUD.add && modo != EstadoCRUD.edit;
        }

        private ObservableCollection<Report> listado;
        public ObservableCollection<Report> Listado {
            get {
                return listado;
            }
        }
        private Report seleccionado;
        public Report Seleccionado {
            get {
                return seleccionado;
            }
            set {
                if (seleccionado != value) {
                    seleccionado = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private Report elemento;
        public Report Elemento {
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

        private EstadoCRUD modo = EstadoCRUD.list;
        public EstadoCRUD Modo {
            get {
                return modo;
            }
            set {
                if (modo != value) {
                    modo = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(EsList));
                    NotifyPropertyChanged(nameof(EsAdd));
                    NotifyPropertyChanged(nameof(EsEdit));
                    NotifyPropertyChanged(nameof(EsView));
                }
            }
        }

        public bool EsList => modo == EstadoCRUD.list;
        public bool EsAdd => modo == EstadoCRUD.add;
        public bool EsEdit => modo == EstadoCRUD.edit;
        public bool EsView => modo == EstadoCRUD.view;

        public ObservableCollection<Category> ListadoCategorias {
            get {
                return new ObservableCollection<Category>((new CategoryRepository()).GetAll());
            }
        }

        public DelegateCommand List {
            get {
                return new DelegateCommand(cmd => {
                    listado = new ObservableCollection<Report>(db.GetAll());
                    NotifyPropertyChanged(nameof(Listado));
                    Seleccionado = listado.FirstOrDefault();
                    Modo = EstadoCRUD.list;
                });
            }
        }

        public DelegateCommand Add {
            get {
                return new DelegateCommand(cmd => {
                    Elemento = new Report();
                    Modo = EstadoCRUD.add;
                });
            }
        }
        public DelegateCommand Edit {
            get {
                return new DelegateCommand(cmd => {
                    if (cmd != null) {
                        Elemento = db.GetById((int)cmd);
                        Modo = EstadoCRUD.edit;
                    }
                });
            }
        }
        public DelegateCommand View {
            get {
                return new DelegateCommand(cmd => {
                    if (cmd != null) {
                        Elemento = db.GetById((int)cmd);
                        Modo = EstadoCRUD.view;
                    }
                });
            }
        }
        public DelegateCommand Delete {
            get {
                return new DelegateCommand(cmd => {
                    if (cmd != null) {
                        db.Delete((int)cmd);
                        //Modo = EstadoCRUD.delete;
                        List.Execute();
                    }
                });
            }
        }
        public DelegateCommand Cancel {
            get {
                return new DelegateCommand(cmd => {
                    Elemento = null;
                    List.Execute();
                });
            }
        }
        public DelegateCommand Accept {
            get {
                return new DelegateCommand(cmd => {
                    switch(Modo) {
                        case EstadoCRUD.add:
                            db.Add(Elemento);
                            Cancel.Execute();
                            break;
                        case EstadoCRUD.edit:
                            db.Modify(Elemento);
                            Cancel.Execute();
                            break;
                    }
                });
            }
        }
        public DelegateCommand Close {
            get {
                return new DelegateCommand(cmd => {
                    OnClosed();
                });
            }
        }
        public DelegateCommand AddEmpleado {
            get {
                return new DelegateCommand(cmd => {
                    if(Elemento != null && cmd is Employee)
                        Elemento.Employees.Add(cmd as Employee);
                });
            }
        }

    }
}
