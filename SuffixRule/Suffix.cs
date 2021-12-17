using RenameLib;
using System;
using System.Collections.Generic;
using System.IO;

namespace SuffixRule
{
    public class Suffix : IRule
    {
        public string RuleName { get => "Suffix"; }

        public bool apply(RenameInfo renameInfo)
        {
            try
            {
                if (renameInfo.Suffix.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    throw new Exception($"Prefix must not contain invalid characters");
                }
                foreach (FileInfo fi in renameInfo.OriginFiles)
                {
                    if (fi.Name.Length + renameInfo.Suffix.Length > 255)
                    {
                        throw new Exception($"Must not exceed 255 characters: {fi.Name}");
                    }
                }
                // if everything is okay
                int index = 0;
                List<FileInfo> newFiles = new List<FileInfo>();
                foreach (FileInfo fi in renameInfo.OriginFiles)
                {
                    string nameWithoutExt = Path.GetFileNameWithoutExtension(fi.Name);
                    string newPath = $"{fi.DirectoryName}\\{nameWithoutExt}{renameInfo.Suffix}{fi.Extension}";
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
