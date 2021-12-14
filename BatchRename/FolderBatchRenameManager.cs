using BatchRename.UtilsClass;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace BatchRename
{
    class FolderBatchRenameManager
    {

        private List<DirectoryInfo> FolderList;
        private List<string> NewFolderNames;

        private List<BatchRenameError> errors;
        public int DuplicateMode = 1;

        public FolderBatchRenameManager()
        {
            errors = new List<BatchRenameError>();
            FolderList = new List<DirectoryInfo>();
            NewFolderNames = new List<string>();
        }

        private List<string> GetErrorList()
        {
            List<string> result = new List<string>();
            return result;
        }

        private bool isInErrorList(int index)
        {
            return false;
        }

        public List<FolderObj> BatchRename(List<FolderObj> folderList, List<StringOperation> operations)
        {
            List<FolderObj> result = new List<FolderObj>(folderList);
            return result;
        }

        private bool handleDuplicateFolder()
        {            
            return false;
        }


        public void CommitChange()
        {
        }
    }
}
