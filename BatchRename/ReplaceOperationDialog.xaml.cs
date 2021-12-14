using System.Windows;

namespace BatchRename
{
    public partial class ReplaceStringDialog : Window
    {
        public delegate void OptArgsDelegate (string From, string To);
        public event OptArgsDelegate OptArgsChange = null;

        public ReplaceStringDialog()
        {
            InitializeComponent();
            FromTextBox.Text = "";
            ToTextBox.Text = "";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (OptArgsChange != null)
            {
                OptArgsChange.Invoke(FromTextBox.Text, ToTextBox.Text);
            }
            DialogResult = true;
            Close();
        }
    }
}
