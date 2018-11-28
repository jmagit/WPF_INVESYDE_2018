using Aplication.Core;
using Aplication.Services.ViewModel;
using Gestion.Views;
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

namespace Gestion {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        private void abrir(UserControl uc, object vm = null) {
            if (vm != null) {
                uc.DataContext = vm;
            }
            if (uc.DataContext is IClosed)
                (uc.DataContext as IClosed).Closed += (s, ev) => {
                    if (!(uc.DataContext is ICanClosed) || (uc.DataContext as ICanClosed).CanClosed())
                        Cerrar();
                };
            root.Content = uc;
        }

        private void Cerrar() {
            root.Content = null;
        }

        private void btnClientes_Click(object sender, RoutedEventArgs e) {
            abrir(new ClientesMnt());
        }

        private void btnOtro_Click(object sender, RoutedEventArgs e) {
            var vm = new ClienteViewModel();
            vm.PropertyChanged += (s, ev) => {
                if (ev.PropertyName == nameof(ClienteViewModel.Modo))
                    switch (vm.Modo) {
                        case EstadoCRUD.list:
                            abrir(new ClienteLst(), vm);
                            break;
                        case EstadoCRUD.add:
                        case EstadoCRUD.edit:
                            abrir(new ClienteForm(), vm);
                            break;
                        case EstadoCRUD.view:
                            abrir(new ClienteView(), vm);
                            break;
                    }
            };
            abrir(new ClienteLst(), vm);
            vm.List.Execute();
        }
    }
}
