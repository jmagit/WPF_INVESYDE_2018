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
    /// Lógica de interacción para CategoriasUC.xaml
    /// </summary>
    public partial class CategoriasUC : UserControl {
        public CategoriasUC() {
            InitializeComponent();
        }



        public bool Cheked {
            get { return (bool)GetValue(ChekedProperty); }
            set { SetValue(ChekedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cheked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChekedProperty =
            DependencyProperty.Register("Cheked", typeof(bool), typeof(CategoriasUC), 
                new PropertyMetadata(true));



    }
}
