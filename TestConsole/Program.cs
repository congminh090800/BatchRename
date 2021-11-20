using RenameLib;
using System;
using System.IO;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load dlls
            DllLoader.execute();
            Console.WriteLine("dll(s) loaded");

            Console.WriteLine("Welcome to the playground");

            // Set up dummies
            FileInfo dummy = new FileInfo(@"D:\dummy.txt");
            FileInfo dummyFolder = new FileInfo(@"D:\dummyfolder");
            RenameInfo renameInfo = new RenameInfo();
            renameInfo.getAllFilesAndDirectory(new FileInfo[] { dummy, dummyFolder }, true); // if recursive mode is on it will scann through all files in folders

            // Change file extension
            IRule changeExtRule = DllLoader.Rules.Find(plugin => plugin.RuleName == "ChangeExtension");
            renameInfo.NewExtension = "txt";
            //try
            //{
            //    Boolean result = changeExtRule.apply(renameInfo); // return true if success
            //} catch (Exception error)
            //{
            //    Console.WriteLine(error.Message);
            //}

            // Trim file name
            IRule trimRule = DllLoader.Rules.Find(plugin => plugin.RuleName == "Trim");
            //try
            //{
            //    Boolean result = trimRule.apply(renameInfo); // return true if success
            //}
            //catch (Exception error)
            //{
            //    Console.WriteLine(error.Message);
            //}

            // Add suffix counter
            IRule counterRule = DllLoader.Rules.Find(plugin => plugin.RuleName == "Counter");
            //try
            //{
            //    Boolean result = counterRule.apply(renameInfo); // return true if success
            //}
            //catch (Exception error)
            //{
            //    Console.WriteLine(error.Message);
            //}

            // Replace by regex or string
            IRule replaceRule = DllLoader.Rules.Find(plugin => plugin.RuleName == "Replace");
            renameInfo.RegexPattern = "file";
            renameInfo.Replacer = "newfile";
            //try
            //{
            //    Boolean result = replaceRule.apply(renameInfo); // return true if success
            //}
            //catch (Exception error)
            //{
            //    Console.WriteLine(error.Message);
            //}

            // Add suffix
            IRule suffixRule = DllLoader.Rules.Find(plugin => plugin.RuleName == "Suffix");
            renameInfo.Suffix = "suffix";
            //try
            //{
            //    Boolean result = suffixRule.apply(renameInfo);
            //} catch (Exception error)
            //{
            //    Console.WriteLine(error.Message);
            //}

            // Add prefix
            IRule prefixRule = DllLoader.Rules.Find(plugin => plugin.RuleName == "Prefix");
            renameInfo.Prefix = "prefix";
            //try
            //{
            //    Boolean result = prefixRule.apply(renameInfo);
            //}
            //catch (Exception error)
            //{
            //    Console.WriteLine(error.Message);
            //}

            // Lowercase and nospace
            IRule lwnsRule = DllLoader.Rules.Find(plugin => plugin.RuleName == "LowerCaseAndNoSpace");
            //try
            //{
            //    Boolean result = lwnsRule.apply(renameInfo);
            //}
            //catch (Exception error)
            //{
            //    Console.WriteLine(error.Message);
            //}

            // Pascal case
            IRule pascalRule = DllLoader.Rules.Find(plugin => plugin.RuleName == "PascalCase");
            renameInfo.PascalCaseSeperator = "-";
            try
            {
                Boolean result = pascalRule.apply(renameInfo);
            } catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}
