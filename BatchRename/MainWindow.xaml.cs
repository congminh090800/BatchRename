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
            fetchFilesWorker.RunWorkerCompleted += RunWorkerCompleted;

            //Create exclude files worker to invoke on click
            removeFilesWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true,
            };
            removeFilesWorker.DoWork += RemoveFiles_DoWork;
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
            Mouse.OverrideCursor = null;

            EnableLoadingViews();
        }

        private void FetchFiles_DoWork(object sender, DoWorkEventArgs e)
        {
            bool willGetSubFiles = false;
            Dispatcher.Invoke(() =>
            {
                willGetSubFiles = (bool)subFileCheck.IsChecked;
            });
            string path = (string)e.Argument + "\\";
            List<string> listPath = new List<string>();
            if (willGetSubFiles)
            {
                DirectoryInfo info = new DirectoryInfo(path);
                FileInfo[] fileInfos = info.GetFiles("*", SearchOption.AllDirectories);
                listPath.Clear();
                for (int i = 0; i < fileInfos.Length; i++)
                {
                    listPath.Add(fileInfos[i].FullName);
                }
            } else
            {
                listPath = Directory.GetFiles(path).ToList();
            }
            string[] children = listPath.ToArray();
            for (int child = 0; child < children.Length; child++)
            {
                bool isDuplicated = false;
                string childName = children[child].Remove(0, path.Length);

                //Check duplicates
                for (int i = 0; i < filesList.Count; i++)
                {
                    if (filesList[i].FullName.Equals(children[child]))
                    {
                        isDuplicated = true;
                        break;
                    }
                }

                if (!isDuplicated)
                {
                    Dispatcher.Invoke(() =>
                    {
                        FileInfo file = new FileInfo(children[child]);
                        filesList.Add(file);
                    });
                }
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
            }
        }

        private void refresh()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
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
            
            for(int i = 0; i < CurrentPresetElements.Count; i++)
            {
                PresetElement element = CurrentPresetElements.ElementAt(i);
                IRule rule = DllLoader.Rules.Find(plugin => plugin.RuleName == element.Name);
                RenameInfo tempInfo = new RenameInfo();
                tempInfo.OriginFiles = fileInfos;
                switch (rule.RuleName) 
                {
                    case "ChangeExtension":
                        tempInfo.NewExtension = element.Params["newExtension"];
                        break;
                    case "PascalCase":
                        tempInfo.PascalCaseSeperator = element.Params["pascalCaseSeperator"];
                        break;
                    case "Prefix":
                        tempInfo.Prefix = element.Params["prefix"];
                        break;
                    case "Replace":
                        tempInfo.RegexPattern = element.Params["regexPattern"];
                        tempInfo.Replacer = element.Params["replacer"];
                        break;
                    case "Suffix":
                        tempInfo.Suffix = element.Params["suffix"];
                        break;
                    default:
                        break;
                }

                rule.apply(tempInfo);
                fileInfos = tempInfo.OriginFiles;
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
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "Choose files";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
                FileInfo file = new FileInfo(openFileDialog.FileName);
                if (filesList.Any(e => e.FullName.Equals(file.FullName))) return;
                filesList.Add(file);
            };
        }

        private void AddFolderButton_Click(object sender, RoutedEventArgs e)
        {
            if (fetchFilesWorker.IsBusy || removeFilesWorker.IsBusy) return;
            var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                DisableLoadingViews();
                fetchFilesWorker.RunWorkerAsync(dialog.SelectedPath);
            }
        }

        private void RemoveFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (fetchFilesWorker.IsBusy || removeFilesWorker.IsBusy || filesList.Count <= 0) return;
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            DisableLoadingViews();
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
        private void MethodMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem tb = sender as MenuItem;
            string ruleName = tb.Header.ToString();
            Trace.WriteLine("name: " + ruleName);
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
                case "Trim":
                    PresetElement trimElement = new PresetElement();
                    trimElement.Name = "Trim";
                    trimElement.Description = "Remove all spaces at head and tail";
                    CurrentPresetElements.Add(trimElement);
                    break;
                default:
                    break;
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
