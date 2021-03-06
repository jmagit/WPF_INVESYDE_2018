﻿using Aplication.Core;
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
    public class ClienteViewModel: ObservableBase, IClosed, ICanClosed {
        private IClienteRepository db = new ClienteRepository();

        public event EventHandler<CancelEventArgs> Closed;
        public event EventHandler<EventArgs> Aceptado;
        public event EventHandler<EventArgs> Cancelado;

        protected virtual void OnClosed() {
            Closed?.Invoke(this, new CancelEventArgs());
        }
        protected virtual void OnClosed(ref CancelEventArgs e) {
            Closed?.Invoke(this, e);
        }
        protected virtual void OnAceptado() {
            Aceptado?.Invoke(this, new EventArgs());
        }
        protected virtual void OnCancelado() {
            Cancelado?.Invoke(this, new EventArgs());
        }

        public bool CanClosed() {
            return modo != EstadoCRUD.add && modo != EstadoCRUD.edit;
        }

        private ObservableCollection<Cliente> listado;
        public ObservableCollection<Cliente> Listado {
            get {
                return listado;
            }
        }
        private Cliente seleccionado;
        public Cliente Seleccionado {
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

        public DelegateCommand List {
            get {
                return new DelegateCommand(cmd => {
                    listado = new ObservableCollection<Cliente>(db.GetAll());
                    NotifyPropertyChanged(nameof(Listado));
                    Seleccionado = listado.FirstOrDefault();
                    Modo = EstadoCRUD.list;
                });
            }
        }

        public DelegateCommand Add {
            get {
                return new DelegateCommand(cmd => {
                    Elemento = new Cliente();
                    Modo = EstadoCRUD.add;
                    accept = new DelegateCommand(x => {
                        db.Add(Elemento);
                        cancelEdit();
                        OnAceptado();
                    });
                    NotifyPropertyChanged(nameof(Accept));
                });
            }
        }
        public DelegateCommand Edit {
            get {
                return new DelegateCommand(cmd => {
                    if (cmd != null) {
                        Elemento = db.GetById((int)cmd);
                        Modo = EstadoCRUD.edit;
                        accept = new DelegateCommand(x => {
                            db.Modify(Elemento);
                            cancelEdit();
                            OnAceptado();
                        });
                        NotifyPropertyChanged(nameof(Accept));
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
        protected void cancelEdit() {
                    Elemento = null;
                    List.Execute();
                    accept = null;
        }
        public DelegateCommand Cancel {
            get {
                return new DelegateCommand(cmd => {
                    cancelEdit();
                    OnCancelado();
                });
            }
        }
        public DelegateCommand Accept {
            get {
                return accept;
                //return new DelegateCommand(cmd => {
                //    switch(Modo) {
                //        case EstadoCRUD.add:
                //            db.Add(Elemento);
                //            Cancel.Execute();
                //            break;
                //        case EstadoCRUD.edit:
                //            db.Modify(Elemento);
                //            Cancel.Execute();
                //            break;
                //    }
                //});
            }
        }
        protected DelegateCommand accept = null;

        public DelegateCommand Close {
            get {
                return new DelegateCommand(cmd => {
                    OnClosed();
                });
            }
        }
    }
}
