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

namespace Gestion.Views {
    /// <summary>
    /// Lógica de interacción para ClienteWizard.xaml
    /// </summary>
    public partial class ClienteWizard : UserControl {
        private List<Panel> paneles = new List<Panel>();
        private int actual = -1;

        public ClienteWizard() {
            InitializeComponent();
            initWizard();
        }

        private void initWizard() {
            foreach(var p in (this.Content as Panel).Children) {
                if(p is Panel && (p as Panel).Tag?.ToString() == "Wizard") {
                    paneles.Add((p as Panel));
                    (p as Panel).Visibility = Visibility.Collapsed;
                }
            }
            if(paneles.Count == 0) {
                btnAnterior.Visibility = Visibility.Collapsed;
                btnPosterior.Visibility = Visibility.Collapsed;
            } else {
                cambia(1);
            }
        }

        private void cambia(int delta) {
            if(actual >= 0) paneles[actual].Visibility = Visibility.Collapsed;
            actual += delta;
            paneles[actual].Visibility = Visibility.Visible;
            btnAnterior.Visibility = actual <= 0 ? Visibility.Hidden : Visibility.Visible;
            btnPosterior.Visibility = (actual + 1) >= paneles.Count ? Visibility.Hidden : Visibility.Visible;
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e) {
            cambia(-1);
        }

        private void btnPosterior_Click(object sender, RoutedEventArgs e) {
            cambia(1);
        }
    }
}
