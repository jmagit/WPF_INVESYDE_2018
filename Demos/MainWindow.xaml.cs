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

namespace Demos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //var x = lblNombre?.Content ?? "(vacio)";

            InitializeComponent();

            //lblNombre.Content = "Texto";
            
        }

        private void Label_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            (new canvas()).Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var w = new canvas();
            w.Owner = this;
            w.Show();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            var w = new cdlg();
            w.Owner = this;
            w.ShowDialog();

        }

        kk cntr = null;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(cntr == null)
                cntr =  new kk();
            root.Content = cntr;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            root.Content = new demo2();
        }

        private void btnAbrirPOP_Click(object sender, RoutedEventArgs e)
        {
            popDlg.IsOpen = true;
        }

        private void btnCerrarPOP_Click(object sender, RoutedEventArgs e)
        {
            popDlg.IsOpen = false;
        }
    }
}
