using RenameLib;
using System;
using System.Collections.Generic;
using System.IO;

namespace TrimRule
{
    public class Trim : IRule
    {
        public string RuleName {get => "Trim";}

        public bool apply(RenameInfo renameInfo)
        {
            try
            {
                foreach (FileInfo fi in renameInfo.OriginFiles)
                {
                    if (fi.Name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                    {
                        throw new Exception($"Must not contain invalid characters: {fi.Name}");
                    }
                }
                // if everything is okay
                int index = 0;
                List<FileInfo> newFiles = new List<FileInfo>();
                foreach (FileInfo fi in renameInfo.OriginFiles)
                {
                    string nameWithoutExt = Path.GetFileNameWithoutExtension(fi.Name);
                    string newPath = $"{fi.DirectoryName}\\{nameWithoutExt.Trim()}{fi.Extension}";
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
