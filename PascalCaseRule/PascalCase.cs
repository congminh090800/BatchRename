using RenameLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace PascalCaseRule
{
    public class PascalCase : IRule
    {
        public string RuleName { get => "PascalCase"; }

        public bool apply(RenameInfo renameInfo)
        {
            int index = 0;
            List<FileInfo> newFiles = new List<FileInfo>();
            foreach (FileInfo fi in renameInfo.OriginFiles)
            {
                string nameWithoutExt = Path.GetFileNameWithoutExtension(fi.Name);
                string lowerCaseName = nameWithoutExt.ToLower().Replace(renameInfo.PascalCaseSeperator, " ");
                TextInfo info = CultureInfo.CurrentCulture.TextInfo;
                string pascalName = info.ToTitleCase(lowerCaseName).Replace(" ", string.Empty);
                string newPath = $"{fi.DirectoryName}\\{pascalName}{fi.Extension}";
                File.Move(fi.FullName, newPath);
                newFiles.Add(new FileInfo(newPath));
                index++;
            }
            renameInfo.OriginFiles = newFiles;
            return true;
        }
    }
}
