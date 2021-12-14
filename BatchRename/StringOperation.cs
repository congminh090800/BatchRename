using BatchRename.UtilsClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BatchRename
{
    public class StringOperationPrototype
    {
        private MainWindow AddContext { get; set; }
        private StringOperation prototype;

        public DelegateCommand CreateNewOperation { get; private set; }
        public string Name { get {
              return prototype.Name;}
        }

        public StringOperationPrototype(StringOperation prototypeOperation, Window contextWindow)
        {
            prototype = prototypeOperation;
            AddContext = contextWindow as MainWindow;
            CreateNewOperation = new DelegateCommand(temp => {
                StringOperation tempOperation = prototype.Clone();
                AddContext.operationsList.Add(tempOperation);
                try
                {
                    tempOperation.OpenDialog();
                }
                catch
                {
                    //do nothing
                }
                
            }
            , null);
        }
    }

    public abstract class StringArgs
    {
        
    }

    public abstract class StringOperation: INotifyPropertyChanged
    {
        public StringArgs Args { get; set; }

        public abstract string Name { get;}

        public abstract string Description { get; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public abstract string OperateString(string origin);

        public abstract void OpenDialog();

        public abstract StringOperation Clone();

        public void Notify(string attrib)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(attrib));
        }
    }
}
