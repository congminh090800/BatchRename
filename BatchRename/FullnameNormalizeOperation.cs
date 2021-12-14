using System;
using System.Collections.Generic;
using System.Text;

namespace BatchRename
{
    public class FullnameNormalizeArgs : StringArgs
    {

    }
    public class FullnameNormalizeOperation : StringOperation
    {
        public override string Name => "Fullname normalize";

        public override string Description
        {
            get
            {
                return "Make a standard fullname";
            }
        }

        public override string OperateString(string origin)
        {
            return origin;
        }

        

        public override StringOperation Clone()
        {
            return new FullnameNormalizeOperation();
        }

        public override void OpenDialog()
        {
            throw new Exception();
        }
    }
}
