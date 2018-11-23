using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculadora.Controles {
    /// <summary>
    /// Lógica de interacción para CalculadoraUC.xaml
    /// </summary>
    public partial class CalculadoraUC : UserControl {
        private double acumulado = 0;
        private char operacion = '+';
        private bool limpia = false;

        public CalculadoraUC() {
            InitializeComponent();
        }

        private void btnIni_Click(object sender, RoutedEventArgs e) {
            acumulado = 0;
            operacion = '+';
            limpia = false;
            txtPantalla.Text = "0";
            txtResumen.Text = "";
        }

        private void btnDigito_Click(object sender, RoutedEventArgs e) {
            string dig = (sender as Button).Content.ToString();
            if (limpia || txtPantalla.Text == "0") {
                txtPantalla.Text = dig;
                limpia = false;
            } else
                txtPantalla.Text += dig;
        }

        private void btnComa_Click(object sender, RoutedEventArgs e) {
            if (limpia || txtPantalla.Text == "0") {
                txtPantalla.Text = "0,";
                limpia = false;
            } else
                if (!txtPantalla.Text.Contains(","))
                txtPantalla.Text += ",";
        }

        private void btnRetro_Click(object sender, RoutedEventArgs e) {
            if (limpia || txtPantalla.Text.Length < 2) {
                txtPantalla.Text = "0";
                limpia = false;
            } else
                txtPantalla.Text = txtPantalla.Text.Remove(txtPantalla.Text.Length - 1);
        }

        private void Calcula(string nuevaOp) {
            if(!"+-*/=".Contains(nuevaOp)) { return; }

            double operando = double.Parse(txtPantalla.Text);
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
            txtResumen.Text = operacion == '=' ? "" : $"{txtResumen.Text} {txtPantalla.Text} {nuevaOp}";
            txtPantalla.Text = acumulado.ToString();
            operacion = nuevaOp[0];
            limpia = true;
        }
        private void btnOperacion_Click(object sender, RoutedEventArgs e) {
            Calcula((sender as Button).Content.ToString());
        }

        private void btnSig_Click(object sender, RoutedEventArgs e) {
            double operando = double.Parse(txtPantalla.Text);
            operando = -operando;
            if (limpia) acumulado = -acumulado;
            txtPantalla.Text = operando.ToString();
        }
    }
}
