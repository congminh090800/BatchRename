using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for ChangeExtensionDialog.xaml
    /// </summary>
    public partial class ChangeExtensionDialog : Window, INotifyPropertyChanged
    {
        public string NewExtension { get; set; }
        public delegate void NewExtensionCb(PresetElement presetElement);
        public event NewExtensionCb OnNewExtensionSubmit = null;
        public ChangeExtensionDialog()
        {
            InitializeComponent();
            DataContext = this;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            PresetElement temp = new PresetElement();
            temp.Name = "ChangeExtension";
            temp.Params["newExtension"] = NewExtension;
            temp.Description = PresetElement.ToPrettyString(temp.Params);
            if (OnNewExtensionSubmit != null)
            {
                OnNewExtensionSubmit(temp);
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ExtTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            SaveButton.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
        }
    }
}
