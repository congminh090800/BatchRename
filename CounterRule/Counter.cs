using RenameLib;
using System;
using System.Collections.Generic;
using System.IO;

namespace CounterRule
{
    public class Counter : IRule
    {
        public string RuleName { get => "Counter"; }

        public Boolean apply(RenameInfo renameInfo)
        {
            try
            {
                int counterCheck = 1;
                foreach (FileInfo fi in renameInfo.OriginFiles)
                {
                    string counterCheckStr = counterCheck.ToString();
                    if (counterCheck < 10)
                    {
                        counterCheckStr = "0" + counterCheckStr;
                    }
                    if (fi.Name.Length + counterCheckStr.Length + 1 > 255)
                    {
                        throw new Exception($"Must not exceed 255 characters: {fi.Name}");
                    }
                    counterCheck++;
                }
                // if everything is okay
                int counter = 1;
                int index = 0;
                List<FileInfo> newFiles = new List<FileInfo>();
                foreach (FileInfo fi in renameInfo.OriginFiles)
                {
                    string counterStr = counter.ToString();
                    if (counter < 10)
                    {
                        counterStr = "0" + counterStr;
                    }
                    string nameWithoutExt = Path.GetFileNameWithoutExtension(fi.Name);
                    string newPath = $"{fi.DirectoryName}\\{nameWithoutExt}_{counterStr}{fi.Extension}";
                    File.Move(fi.FullName, newPath);
                    newFiles.Add(new FileInfo(newPath));
                    counter++;
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
