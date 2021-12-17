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
using System.Windows.Shapes;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for PascalCaseDialog.xaml
    /// </summary>
    public partial class PascalCaseDialog : Window
    {
        public delegate void PascalCaseCb(PresetElement presetElement);
        public event PascalCaseCb OnSeperatorSubmit = null;
        public PascalCaseDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            PresetElement temp = new PresetElement();
            temp.Name = "PascalCase";
            temp.Params["pascalCaseSeperator"] = TextBox.Text;
            temp.Description = PresetElement.ToPrettyString(temp.Params);
            if (OnSeperatorSubmit != null)
            {
                OnSeperatorSubmit(temp);
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