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
using System.Xml;
namespace GUINavegacion
{
    /// <summary>
    /// Lógica de interacción para ExpenseReportPage.xaml
    /// </summary>
    public partial class ExpenseReportPage : Page
    {
        public ExpenseReportPage()
        {
            InitializeComponent();
        }
        // Custom constructor to pass expense report data 
        public ExpenseReportPage(object data) : this()
        {
            // Bind to expense report data. 
            this.DataContext = data;
            if(data is XmlElement) {
                this.Title = "Datos: " + (data as XmlElement).GetAttribute("Name");
            }
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

         private void GoBack_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.GoBack();
        }

        string Msg { get; set; }
    }
}
