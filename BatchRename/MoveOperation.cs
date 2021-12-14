using System;

namespace BatchRename
{

    public class MoveArgs : StringArgs
    {
        public string Mode { get; set; }
        public int Number { get; set; }
    }
    public class MoveOperation : StringOperation
    {
        public override string Name => "Move";
 
        public override string Description
        {
            get
            {
                var arg = Args as MoveArgs;
                string result = $"Move {arg.Number} characters to {arg.Mode} of the string";
                return result;
            }
        }

        public MoveOperation()
        {
            Args = new MoveArgs()
            {
                Mode = "Front",
                Number = 0
            };
        }


        public override string OperateString(string origin)
        {
            string result = null;

            switch((Args as MoveArgs).Mode)
            {
                case "Front":
                    {
                        try
                        {
                            result = BringToFront(origin, (Args as MoveArgs).Number);
                            break;
                        }
                        catch (Exception e)
                        {
                            throw new Exception(e.Message);
                        }
                    }
                    
                    
                case "Back":
                    {
                        try
                        {
                            result = BringToBack(origin, (Args as MoveArgs).Number);
                            break;
                        }
                        catch (Exception e)
                        {
                            throw new Exception(e.Message);
                        }
                    }
                   
            }

            return result;
        }

        private static string BringToFront(string origin, int num)
        {
            return origin;
        }
        private static string BringToBack(string origin, int num)
        {
            return origin;
        }

        void ChangeMoveArgs(MoveArgs ChangedArgs)
        {
            Args = ChangedArgs;
        }

        public override void OpenDialog()
        {
            var screen = new MoveOperationDialog(Args);
            screen.StringArgsChange += ChangeMoveArgs;
            if (screen.ShowDialog() == true)
            {
                Notify("Description");
            }
        }

        public override StringOperation Clone()
        {
            var oldArgs = Args as MoveArgs;
            return new MoveOperation()
            {
                Args = new MoveArgs()
                {
                    Mode = oldArgs.Mode,
                    Number = oldArgs.Number
                }
            };
        }
    }
}
