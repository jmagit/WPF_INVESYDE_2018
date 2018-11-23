using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora.ViewModels {
    public class Calculadora : ObservableBase {
        private double acumulado = 0;
        private char operacion = '+';
        private bool limpia = false;

        private string pantalla = "0";
        public string Pantalla {
            get {
                return pantalla;
            }
            set {
                if (pantalla != value) {
                    pantalla = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string resumen = "";
        public string Resumen {
            get {
                return resumen;
            }
            set {
                if (resumen != value) {
                    resumen = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DelegateCommand Inicio {
            get {
                return new DelegateCommand(cmd => {
                    acumulado = 0;
                    operacion = '+';
                    limpia = false;
                    Pantalla = "0";
                    Resumen = "";
                });
            }
        }
        public DelegateCommand Digito {
            get {
                return new DelegateCommand(cmd => {
                    string dig = cmd as string;
                    if (limpia || Pantalla == "0") {
                        Pantalla = dig;
                        limpia = false;
                    } else
                        Pantalla += dig;
                });
            }
        }

        public DelegateCommand Coma {
            get {
                return new DelegateCommand(cmd => {
                    if (limpia || Pantalla == "0") {
                        Pantalla = "0,";
                        limpia = false;
                    } else
                        if (!Pantalla.Contains(","))
                        Pantalla += ",";
                });
            }
        }

        public DelegateCommand Retro {
            get {
                return new DelegateCommand(cmd => {
                    if (limpia || Pantalla.Length < 2) {
                        Pantalla = "0";
                        limpia = false;
                    } else
                        Pantalla = Pantalla.Remove(Pantalla.Length - 1);
                });
            }
        }


        private void Calcula(string nuevaOp) {
            double operando = double.Parse(Pantalla);
            switch (operacion) {
                case '+':
                    acumulado += operando;
                    break;
                case '-':
                    acumulado -= operando;
                    break;
                case '*':
                    acumulado *= operando;
                    break;
                case '/':
                    acumulado /= operando;
                    break;
                case '=':
                    acumulado = operando;
                    break;
            }
            Resumen = operacion == '=' ? "" : $"{Resumen} {Pantalla} {nuevaOp}";
            Pantalla = acumulado.ToString();
            operacion = nuevaOp[0];
            limpia = true;
        }
        public DelegateCommand Operacion {
            get {
                return new DelegateCommand(cmd => {
                    Calcula(cmd as string);
                });
            }
        }

        public DelegateCommand Signo {
            get {
                return new DelegateCommand(cmd => {
                    double operando = double.Parse(Pantalla);
                    operando = -operando;
                    if (limpia) acumulado = -acumulado;
                    Pantalla = operando.ToString();
                });
            }
        }
    }
}
