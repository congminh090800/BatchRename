using RenameLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace LowerCaseAndNoSpaceRule
{
    public class LowerCaseAndNoSpace : IRule
    {
        public string RuleName { get => "LowerCaseAndNoSpace"; }

        public bool apply(RenameInfo renameInfo)
        {
            int index = 0;
            List<FileInfo> newFiles = new List<FileInfo>();
            foreach (FileInfo fi in renameInfo.OriginFiles)
            {
                string nameWithoutExt = Path.GetFileNameWithoutExtension(fi.Name);
                string lowerCaseName = nameWithoutExt.ToLower();
                string noSpaceName = Regex.Replace(lowerCaseName, @"\s+", "");
                string newPath = $"{fi.DirectoryName}\\{noSpaceName}{fi.Extension}";
                File.Move(fi.FullName, newPath);
                newFiles.Add(new FileInfo(newPath));
                index++;
            }
            renameInfo.OriginFiles = newFiles;
            return true;
        }
    }
}
