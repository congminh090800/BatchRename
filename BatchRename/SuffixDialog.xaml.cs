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
    /// Interaction logic for SuffixDialog.xaml
    /// </summary>
    public partial class SuffixDialog : Window, INotifyPropertyChanged
    {
        public string Suffix { get; set; }
        public delegate void SuffixCb(PresetElement presetElement);
        public event SuffixCb OnSuffixSubmit = null;
        public event PropertyChangedEventHandler PropertyChanged;

        public SuffixDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            PresetElement temp = new PresetElement();
            temp.Name = "Suffix";
            temp.Params["suffix"] = Suffix;
            temp.Description = PresetElement.ToPrettyString(temp.Params);
            if (OnSuffixSubmit != null)
            {
                OnSuffixSubmit(temp);
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            SaveButton.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
        }
    }
}
