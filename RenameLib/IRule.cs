
using System;
using System.Collections.Generic;

namespace RenameLib
{
    public interface IRule
    {
        public string RuleName { get; }
        public Boolean apply(RenameInfo renameInfo);
    }
}
