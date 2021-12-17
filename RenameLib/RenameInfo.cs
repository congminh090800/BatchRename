
using System;
using System.Collections.Generic;
using System.IO;

namespace RenameLib
{
    public class RenameInfo
    {
        public RenameInfo()
        {
            OriginFiles = new List<FileInfo>();
            OriginFolders = new List<DirectoryInfo>();
        }
        public List<FileInfo> OriginFiles { get; set; }
        public List<DirectoryInfo> OriginFolders { get; set; }
        public string RegexPattern { get; set; }
        public string Replacer { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string PascalCaseSeperator { get; set; } = " ";
        public void getAllFilesAndDirectory(FileInfo[] files, Boolean recursiveMode)
        {
            OriginFiles = new List<FileInfo>();
            OriginFolders = new List<DirectoryInfo>();
            foreach(FileInfo fi in files)
            {
                if (!File.Exists(fi.FullName) && !Directory.Exists(fi.FullName))
                {
                    continue;
                }
                if ((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    DirectoryInfo info = new DirectoryInfo(fi.FullName);
                    if (!recursiveMode)
                    {
                        OriginFolders.Add(info);
                    } else
                    {
                        FileInfo[] fileInfos = info.GetFiles("*", SearchOption.AllDirectories);
                        DirectoryInfo[] directoryInfos = info.GetDirectories("*", SearchOption.AllDirectories);
                        OriginFiles.AddRange(fileInfos);
                        OriginFolders.AddRange(directoryInfos);
                    }
                } else
                {
                    OriginFiles.Add(fi);
                }
            }
        }
        public string NewExtension { get; set; }
    }
}
