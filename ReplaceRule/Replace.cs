using RenameLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ReplaceRule
{
    public class Replace : IRule
    {
        public string RuleName { get => "Replace"; }

        public bool apply(RenameInfo renameInfo)
        {
            foreach(FileInfo fi in renameInfo.OriginFiles)
            {
                string nameWithoutExt = Path.GetFileNameWithoutExtension(fi.Name);
                string nameAfterReplace = Regex.Replace(nameWithoutExt, renameInfo.RegexPattern, renameInfo.Replacer);
                if (nameAfterReplace.Length > 255)
                {
                    throw new Exception($"Must not exceed 255 characters: {fi.Name}");
                }
                if (nameAfterReplace.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    throw new Exception($"Must not contain invalid characters: {fi.Name}");
                }
            }
            int index = 0;
            List<FileInfo> newFiles = new List<FileInfo>();
            foreach (FileInfo fi in renameInfo.OriginFiles)
            {
                string nameWithoutExt = Path.GetFileNameWithoutExtension(fi.Name);
                string nameAfterReplace = Regex.Replace(nameWithoutExt, renameInfo.RegexPattern, renameInfo.Replacer);
                string newPath = $"{fi.DirectoryName}\\{nameAfterReplace}{fi.Extension}";
                File.Move(fi.FullName, newPath);
                newFiles.Add(new FileInfo(newPath));
                index++;
            }
            renameInfo.OriginFiles = newFiles;
            return true;
        }
    }
}
