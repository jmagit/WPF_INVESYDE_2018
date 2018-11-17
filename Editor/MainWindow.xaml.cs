using System;
using System.Collections.Generic;
using System.IO;
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

namespace Editor {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public string FileName { get; set; }

        public MainWindow() {
            InitializeComponent();
            FileName = null;
        }
        void ApplicationCommandsExecute(object target, ExecutedRoutedEventArgs e) {
            String command, targetobj;
            command = ((RoutedCommand)e.Command).Name;
            targetobj = ((FrameworkElement)target).Name;
            switch(command) {
                case "New":
                    NewMnu(null, null);
                    break;
                case "Open":
                    OpenMnu(null, null);
                    break;
                case "Save":
                    SaveMnu(null, null);
                    break;
                case "SaveAs":
                    SaveAsMnu(null, null);
                    break;
                case "Close":
                    CloseMnu(null, null);
                    break;
            }
        }
        void ApplicationCommandsCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }
        void SaveCmdCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = string.IsNullOrWhiteSpace(FileName);
        }
        void NewMnu(Object sender, RoutedEventArgs args) {
            mainRTB.Document = new FlowDocument();
            FileName=null;
        }
        void OpenMnu(Object sender, RoutedEventArgs args) {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".xaml"; // Default file extension
            dlg.Filter = "XAML Documents (.xaml)|*.xaml"; // Filter files by extension

            // Process open file dialog box results
            if (dlg.ShowDialog() == true) {
                // Open document
                LoadXamlPackage(dlg.FileName);
            }
        }
        void SaveMnu(Object sender, RoutedEventArgs args) {
            if (string.IsNullOrWhiteSpace(FileName))
                SaveAsMnu(sender, args);
            else
                SaveXamlPackage(FileName);
        }
        void SaveAsMnu(Object sender, RoutedEventArgs args) {
            var dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".xaml"; // Default file extension
            dlg.Filter = "XAML Documents (.xaml)|*.xaml"; // Filter files by extension

            // Process open file dialog box results
            if (dlg.ShowDialog() == true) {
                // Open document
                SaveXamlPackage(dlg.FileName);
                FileName = dlg.FileName;
            }
        }
        void CloseMnu(Object sender, RoutedEventArgs args) {
            App.Current.Shutdown();
        }

        void SaveXamlPackage(string _fileName) {
            TextRange range;
            FileStream fStream;
            range = new TextRange(mainRTB.Document.ContentStart, mainRTB.Document.ContentEnd);
            fStream = new FileStream(_fileName, FileMode.Create);
            range.Save(fStream, DataFormats.XamlPackage);
            fStream.Close();
        }

        // Load XAML into RichTextBox from a file specified by _fileName
        void LoadXamlPackage(string _fileName) {
            TextRange range;
            FileStream fStream;
            if (File.Exists(_fileName)) {
                FileName = _fileName;
                range = new TextRange(mainRTB.Document.ContentStart, mainRTB.Document.ContentEnd);
                fStream = new FileStream(_fileName, FileMode.OpenOrCreate);
                range.Load(fStream, DataFormats.XamlPackage);
                fStream.Close();
            }
        }

        // Print RichTextBox content
        private void PrintCommand() {
            PrintDialog pd = new PrintDialog();
            if ((pd.ShowDialog() == true)) {
                //use either one of the below      
                pd.PrintVisual(mainRTB as Visual, "printing as visual");
                pd.PrintDocument((((IDocumentPaginatorSource)mainRTB.Document).DocumentPaginator), "printing as paginator");
            }
        }
    }
}
