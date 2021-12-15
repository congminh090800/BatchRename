using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
        private BackgroundWorker fetchFilesWorker;
        private BackgroundWorker excludeFilesWorker;

        private BindingList<Preset> loadedPresets; //Use for back up preset loaded

        public MainWindow()
        {
            InitializeComponent();

            loadedPresets = new BindingList<Preset>();

            //Bind
            PresetsList.ItemsSource = loadedPresets;

            //Create fetch files worker to invoke on click
            fetchFilesWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            fetchFilesWorker.DoWork += FetchFiles_DoWork;
            fetchFilesWorker.ProgressChanged += ProgressChanged;
            fetchFilesWorker.RunWorkerCompleted += RunWorkerCompleted;

            //Create exclude files worker to invoke on click
            excludeFilesWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true,
            };
            excludeFilesWorker.DoWork += ExcludeFiles_DoWork;
            excludeFilesWorker.ProgressChanged += ProgressChanged;
            excludeFilesWorker.RunWorkerCompleted += RunWorkerCompleted;
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
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Title = "Open";
            //openFileDialog.DefaultExt = "txt";
            //openFileDialog.Filter = "Text files (*.txt)|*.txt";
            //openFileDialog.RestoreDirectory = true;
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    var reader = new StreamReader(openFileDialog.FileName);
            //    Preset preset = new Preset();
            //    var tokens2 = openFileDialog.FileName.Split(new string[] { "\\" }, StringSplitOptions.None);
            //    var tokens3 = tokens2[tokens2.Length - 1].Split(new string[] { "." }, StringSplitOptions.None);
            //    preset.Name = tokens3[0];

            //    reader.Close();
            //}
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            ////saveFileDialog.InitialDirectory = @"C:\";
            //saveFileDialog.DefaultExt = "txt";
            //saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            //saveFileDialog.RestoreDirectory = true;
            //if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    var writer = new StreamWriter(saveFileDialog.FileName);
            //    writer.WriteLine("BatchRename");

            //    writer.Close();
            //}
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
    }
}
