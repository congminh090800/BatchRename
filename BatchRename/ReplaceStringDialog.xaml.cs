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
    /// Interaction logic for ReplaceStringDialog.xaml
    /// </summary>
    public partial class ReplaceStringDialog : Window
    {

        public delegate void ReplaceCb(PresetElement presetElement);
        public event ReplaceCb OnReplaceSubmit = null;

        public ReplaceStringDialog()
        {
            InitializeComponent();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            PresetElement temp = new PresetElement();
            temp.Name = "Replace";
            temp.Params["From"] = FromTextBox.Text;
            temp.Params["To"] = ToTextBox.Text;
            temp.Description = PresetElement.ToPrettyString(temp.Params);
            if (OnReplaceSubmit != null)
            {
                OnReplaceSubmit(temp);
                Close();
            }
        }
    }
}
