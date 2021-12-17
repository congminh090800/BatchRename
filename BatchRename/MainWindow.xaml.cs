using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using RenameLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private BindingList<FileInfo> filesList;
        public List<IRule> Rules;
        public BindingList<PresetElement> CurrentPresetElements { get; set; } = new BindingList<PresetElement>();

        private BackgroundWorker fetchFilesWorker;
        private BackgroundWorker removeFilesWorker;
        private RenameInfo renameInfo;
        public MainWindow()
        {
            DllLoader.execute();
            Rules = DllLoader.Rules;
            InitializeComponent();
            AddMethodButton.ContextMenu.ItemsSource = Rules;
            OperationsList.ItemsSource = CurrentPresetElements;

            renameInfo = new RenameInfo();

            filesList = new BindingList<FileInfo>();

            fetchFilesWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            fetchFilesWorker.DoWork += FetchFiles_DoWork;
            fetchFilesWorker.ProgressChanged += ProgressChanged;
            fetchFilesWorker.RunWorkerCompleted += RunWorkerCompleted;

            //Create exclude files worker to invoke on click
            removeFilesWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true,
            };
            removeFilesWorker.DoWork += RemoveFiles_DoWork;
            removeFilesWorker.ProgressChanged += ProgressChanged;
            removeFilesWorker.RunWorkerCompleted += RunWorkerCompleted;

            RenameFilesList.ItemsSource = filesList;
        }

        private void DisableLoadingViews()
        {
            AddFileButton.IsEnabled = false;
            RemoveFileButton.IsEnabled = false;
            StartButton.IsEnabled = false;
        }

        private void EnableLoadingViews()
        {
            AddFileButton.IsEnabled = true;
            RemoveFileButton.IsEnabled = true;
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
            string path = (string)e.Argument + "\\";
            var children = Directory.GetFiles(path);
            StringBuilder output = new StringBuilder();

            for (int child = 0; child < children.Length; child++)
            {
                bool isDuplicated = false;
                string childName = children[child].Remove(0, path.Length);

                //Check duplicates
                for (int i = 0; i < filesList.Count; i++)
                {
                    if (filesList[i].Name.Equals(childName) && filesList[i].FullName.Equals(path))
                    {
                        isDuplicated = true;
                        break;
                    }
                }

                output.Clear();
                string result = "Skip duplicate ";
                if (!isDuplicated)
                {
                    result = "Add ";
                    Dispatcher.Invoke(() =>
                    {
                        FileInfo file = new FileInfo(children[child]);
                        filesList.Add(file);
                    });
                }
                output.Append(result);
                output.Append(path);
                output.Append(childName);

                fetchFilesWorker.ReportProgress(child * 100 / children.Length, output.ToString());
            }
        }

        private void RemoveFiles_DoWork(object sender, DoWorkEventArgs e)
        {
            var items = ((IList<object>)e.Argument).Cast<FileInfo>().ToList();

            int amount = items.Count;

            StringBuilder output = new StringBuilder();

            for (int item = 0; item < amount; item++)
            {
                Dispatcher.Invoke(() => {
                    filesList.Remove(items[item]);
                });
                output.Clear();
                output.Append("Remove ");
                output.Append(items[item].FullName + items[item].Name);
                removeFilesWorker.ReportProgress(item * 100 / amount, output.ToString());
            }
        }

        private void refresh()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            LoadingBar.Value = 0;
            LoadingOutput.Text = "";
            Mouse.OverrideCursor = null;
            filesList.Clear();
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
            List<FileInfo> fileInfos = new List<FileInfo>();
            foreach (FileInfo file in filesList)
            {
                fileInfos.Add(file);
            }
            renameInfo.OriginFiles = fileInfos;
            
            for(int i = 0; i < CurrentPresetElements.Count; i++)
            {
                PresetElement element = CurrentPresetElements.ElementAt(i);
                IRule rule = DllLoader.Rules.Find(plugin => plugin.RuleName == element.Name);
                switch (rule.RuleName) 
                {
                    case "ChangeExtension":
                        renameInfo.NewExtension = element.Params["newExtension"];
                        break;
                    case "PascalCase":
                        renameInfo.PascalCaseSeperator = element.Params["pascalCaseSeperator"];
                        break;
                    case "Prefix":
                        renameInfo.Prefix = element.Params["prefix"];
                        break;
                    case "Replace":
                        renameInfo.RegexPattern = element.Params["regexPattern"];
                        renameInfo.Replacer = element.Params["replacer"];
                        break;
                    case "Suffix":
                        renameInfo.Suffix = element.Params["suffix"];
                        break;
                    default:
                        break;
                }

                rule.apply(renameInfo);
            }

            CompletedRenameDialog notiDialog = new CompletedRenameDialog();
            notiDialog.ShowDialog();
            refresh();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
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
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
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
            if (fetchFilesWorker.IsBusy || removeFilesWorker.IsBusy) return;
            var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                DisableLoadingViews();
                LoadingBar.Value = 0;
                Debug.WriteLine("path: " + dialog.SelectedPath);
                fetchFilesWorker.RunWorkerAsync(dialog.SelectedPath);
            }
        }

        private void RemoveFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (fetchFilesWorker.IsBusy || removeFilesWorker.IsBusy || filesList.Count <= 0) return;
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            DisableLoadingViews();
            LoadingBar.Value = 0;
            removeFilesWorker.RunWorkerAsync(RenameFilesList.SelectedItems);
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
            var item = OperationsList.SelectedItem as PresetElement;
            if (item != null)
            {
                CurrentPresetElements.Remove(item);
                CurrentPresetElements.Add(item);
                OperationsList.SelectedIndex = CurrentPresetElements.Count - 1;
            }
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            var item = OperationsList.SelectedItem as PresetElement;
            if (item != null)
            {
                int index = CurrentPresetElements.IndexOf(item);
                if (index < CurrentPresetElements.Count - 1)
                {
                    CurrentPresetElements.RemoveAt(index);
                    CurrentPresetElements.Insert(index + 1, item);
                    OperationsList.SelectedIndex = index + 1;
                }
            }
        }

        private void UpTopButton_Click(object sender, RoutedEventArgs e)
        {
            var item = OperationsList.SelectedItem as PresetElement;
            if (item != null)
            {
                CurrentPresetElements.Remove(item);
                CurrentPresetElements.Insert(0, item);
                OperationsList.SelectedIndex = 0;
            }
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            var item = OperationsList.SelectedItem as PresetElement;
            if (item != null)
            {
                int index = CurrentPresetElements.IndexOf(item);
                if (index > 0)
                {
                    CurrentPresetElements.RemoveAt(index);
                    CurrentPresetElements.Insert(index - 1, item);
                    OperationsList.SelectedIndex = index - 1;
                }
            }
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
                if (CurrentPresetElements.Any(e => e.Name.Equals(ruleName))){
                    return;
                }
                    switch (ruleName)
                {
                    case "ChangeExtension":
                        openChangeExtDialog();
                        break;
                    case "Counter":
                        PresetElement counterElement = new PresetElement();
                        counterElement.Name = "Counter";
                        counterElement.Description = "Add counter to the end of the file";
                        CurrentPresetElements.Add(counterElement);
                        break;
                    case "LowerCaseAndNoSpace":
                        PresetElement lowerCaseAndNoSpacElement = new PresetElement();
                        lowerCaseAndNoSpacElement.Name = "LowerCaseAndNoSpace";
                        lowerCaseAndNoSpacElement.Description = "Convert all characters to lowercase, remove all spaces";
                        CurrentPresetElements.Add(lowerCaseAndNoSpacElement);
                        break;
                    case "PascalCase":
                        openPascalCaseDialog();
                        break;
                    case "Prefix":
                        openPrefixDialog();
                        break;
                    case "Replace":
                        openReplaceDialog();
                        break;
                    case "Suffix":
                        openSuffixDialog();
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

        public void openReplaceDialog()
        {
            ReplaceStringDialog window = new ReplaceStringDialog();
            window.OnReplaceSubmit += (presetElement) =>
            {
                CurrentPresetElements.Add(presetElement);
            };
            window.Show();
        }

        public void openPascalCaseDialog()
        {
            PascalCaseDialog window = new PascalCaseDialog();
            window.OnSeperatorSubmit += (presetElement) =>
            {
                CurrentPresetElements.Add(presetElement);
            };
            window.Show();
        }

        public void openPrefixDialog()
        {
            PrefixDialog window = new PrefixDialog();
            window.OnPrefixSubmit += (presetElement) =>
            {
                CurrentPresetElements.Add(presetElement);
            };
            window.Show();
        }

        public void openSuffixDialog()
        {
            SuffixDialog window = new SuffixDialog();
            window.OnSuffixSubmit += (presetElement) =>
            {
                CurrentPresetElements.Add(presetElement);
            };
            window.Show();
        }
    }
}
