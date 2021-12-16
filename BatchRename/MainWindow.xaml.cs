using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using RenameLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<IRule> Rules;
        public BindingList<PresetElement> CurrentPresetElements { get; set; } = new BindingList<PresetElement>();
        public MainWindow()
        {
            DllLoader.execute();
            Rules = DllLoader.Rules;
            InitializeComponent();
            AddMethodButton.ContextMenu.ItemsSource = Rules;
            OperationsList.ItemsSource = CurrentPresetElements;
        }

        private void DisableLoadingViews()
        {
            AddFileButton.IsEnabled = false;
            ExcludeFileButton.IsEnabled = false;
            StartButton.IsEnabled = false;
        }

        private void EnableLoadingViews()
        {
            AddFileButton.IsEnabled = true;
            ExcludeFileButton.IsEnabled = true;
            StartButton.IsEnabled = true;
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadingBar.Value = 100;
            Mouse.OverrideCursor = null;
            LoadingOutput.Text = "Action completed";

            EnableLoadingViews();
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            LoadingBar.Value = e.ProgressPercentage;
            if (e.UserState != null)
                LoadingOutput.Text = (string)e.UserState;
        }

        private void FetchFiles_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void ExcludeFiles_DoWork(object sender, DoWorkEventArgs e)
        {

        }


        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            LoadingBar.Value = 0;
            LoadingOutput.Text = "";
            Mouse.OverrideCursor = null;
        }

        private void AddMethodButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement addButton)
            {
                addButton.ContextMenu.PlacementTarget = addButton;
                addButton.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                addButton.ContextMenu.MinWidth = addButton.ActualWidth;
                addButton.ContextMenu.MinHeight = 30;
                addButton.ContextMenu.Margin = new Thickness(0, 5, 0, 0);
                addButton.ContextMenu.IsOpen = true;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            CompletedRenameDialog notiDialog = new CompletedRenameDialog();
            notiDialog.ShowDialog();
            RefreshButton_Click(sender, e);
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Load preset";
            openFileDialog.DefaultExt = "json";
            openFileDialog.Filter = "Json files (*.json)|*.json";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
                string jsonString = File.ReadAllText(openFileDialog.FileName);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                CurrentPresetElements = JsonSerializer.Deserialize<BindingList<PresetElement>>(jsonString, options);
                OperationsList.ItemsSource = CurrentPresetElements;
            };
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.Filter = "Json files (*.json)|*.json";
            saveFileDialog.Title = "Save preset";
            saveFileDialog.RestoreDirectory = true;
            Nullable<bool> result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                String fileName = saveFileDialog.FileName;
                string jsonString = JsonSerializer.Serialize(CurrentPresetElements);
                string beautified = JToken.Parse(jsonString).ToString(Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(fileName, beautified);
            }
        }

        private void AddFileButton_Click(object sender, RoutedEventArgs e)
        {
            //if (fetchFilesWorker.IsBusy || excludeFilesWorker.IsBusy) return;
            //var dialog = new FolderBrowserDialog();

            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            //    DisableLoadingViews();
            //    LoadingBar.Value = 0;
            //    fetchFilesWorker.RunWorkerAsync(dialog.SelectedPath);
            //}

        }

        private void ExcludeFileButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            PresetElement item = ((sender as System.Windows.Controls.Button).Tag as PresetElement);
            CurrentPresetElements.Remove(item);
        }

        private void RenameTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PresetsList_DropDownClosed(object sender, EventArgs e)
        {

        }

        void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb senderAsThumb = e.OriginalSource as Thumb;
            GridViewColumnHeader header
                = senderAsThumb?.TemplatedParent as GridViewColumnHeader;
            if (header?.Column.ActualWidth < header?.MinWidth)
            {
                header.Column.Width = header.MinWidth;
            }
        }

        private void DownBottomButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpTopButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock)
            {
                TextBlock tb = e.Source as TextBlock;
                string ruleName = tb.Text;
                switch(ruleName)
                {
                    case "ChangeExtension":
                        openChangeExtDialog();
                        break;
                    default:
                        break;
                }
            }
        }
        public void openChangeExtDialog()
        {
            ChangeExtensionDialog window = new ChangeExtensionDialog();
            window.OnNewExtensionSubmit += (presetElement) =>
            {
                CurrentPresetElements.Add(presetElement);
            };
            window.Show();
        }
    }
}
