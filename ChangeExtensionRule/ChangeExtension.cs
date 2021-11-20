using RenameLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace ChangeExtensionRule
{
    public class ChangeExtension : IRule
    {
        public string RuleName { get => "ChangeExtension"; }
        public Boolean apply(RenameInfo renameInfo)
        {
            try
            {
                Regex regex = new Regex(@"^[a-zA-Z0-9]+$");
                foreach (FileInfo fi in renameInfo.OriginFiles)
                {
                    if (!regex.IsMatch(renameInfo.NewExtension))
                    {
                        throw new Exception($"Extension must contains only numbers and letters: {fi.Name}");
                    }
                    if (fi.Name.Length + renameInfo.NewExtension.Length - fi.Extension.Length > 255)
                    {
                        throw new Exception($"Must not exceed 255 characters: {fi.Name}");
                    }
                }
                // if everything is okay
                int index = 0;
                List<FileInfo> newFiles = new List<FileInfo>();
                foreach (FileInfo fi in renameInfo.OriginFiles)
                {
                    string newPath = Path.ChangeExtension(fi.FullName, renameInfo.NewExtension);
                    File.Move(fi.FullName, newPath);
                    newFiles.Add(new FileInfo(newPath));
                    index++;
                }
                renameInfo.OriginFiles = newFiles;
                return true;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }

        }
    }
}
