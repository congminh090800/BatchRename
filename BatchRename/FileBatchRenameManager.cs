using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using BatchRename.UtilsClass;

namespace BatchRename
{
    class BatchRenameError
    {
        public int NameErrorIndex { get; set; }
        public string LastNameValue { get; set; }
        public string Message { get; set; }
    }


    class FileBatchRenameManager
    {
        private List<FileInfo> FileList;
        private List<string> NewFileNames;

        private List<BatchRenameError> errors;
        public int DuplicateMode = 1;
        public FileBatchRenameManager()
        {

            errors = new List<BatchRenameError>();
            FileList = new List<FileInfo>();
            NewFileNames = new List<string>();
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

        public List<FileObj> BatchRename(List<FileObj> fileList, List<StringOperation> operations)
        {
            List<FileObj> result = new List<FileObj>(fileList);
            
            return result;

        }


        private bool handleDuplicateFiles()
        {
            return false;
            
        }


        public void CommitChange()
        {
        }
    }

    class Preset
    {
        public string Name { get; set; }
        public BindingList<StringOperation> stringOperations { get; set; }
        public Preset()
        {
            Name = "";
            stringOperations = new BindingList<StringOperation>();
        }
    }


}
