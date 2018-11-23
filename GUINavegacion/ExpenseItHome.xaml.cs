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

namespace GUINavegacion {
    /// <summary>
    /// Lógica de interacción para ExpenseItHome.xaml
    /// </summary>
    public partial class ExpenseItHome : Page {
        string Msg { get; set; }
        public ExpenseItHome() {
            InitializeComponent();
            this.Loaded += (sender, ev) => {
                this.NavigationService.Navigated += (s, e) => {
                    Msg = "";
                };
                this.NavigationService.Navigating += (s, e) => {
                    Msg = "";
                };
                this.NavigationService.NavigationFailed += (s, e) => {
                    Msg = "";
                };
                this.NavigationService.NavigationProgress += (s, e) => {
                    Msg = "";
                };
                this.NavigationService.NavigationStopped += (s, e) => {
                    Msg = "";
                };
            };
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
            // View Expense Report 
            pagina = new ExpenseReportPage(this.peopleListBox.SelectedItem);
            this.NavigationService.Navigate(pagina);
        }

        Page pagina;
    }
}
