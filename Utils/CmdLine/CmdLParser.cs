using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceRandomizer.Utils.CmdLine
{
    class CmdParser
    {
        /// <summary>
        /// Adds a argument to the arglist
        /// </summary>
        /// <returns>idx of the item in the list</returns>
        public static void AddCmdArg(CmdArg arg) { argList.Add(arg); }

        public static CmdArg GetCmdArg(int idx) { return argList[idx]; }
        public static List<CmdArg> GetCmdList() { return argList; }

        /// <summary>
        /// Will Parse the given arguments and bitch if sth is off
        /// </summary>
        /// <param name="args">Command line args</param>
        /// <returns>if the parsing was successfull (!malformed)</returns>
        public static bool ParseArgs(string[] args)
        {
            List<string> argsList = args.ToList();

            foreach(CmdArg arg in argList)
            {
                if (!arg.ParseArg(ref argsList))
                    return false;
            }

            return true;
        }

        #region Command Line Args
        public static CmdArg folderLocationArg;
        public static CmdArg fileLocationArg;
        static CmdArg HelpArg;
        static bool DoHelp(CmdArg x)
        {
            foreach (CmdArg arg in CmdParser.GetCmdList())
            {
                Console.WriteLine(arg.GetShortCommand() + " " + arg.GetLongCommand() + "\t" + arg.GetDescription());
            }
            return false;
        }

        public static void Init()
        {
            folderLocationArg = new CmdArg("-d", "--directory", "the dir where the sauce is located", 1);
            fileLocationArg = new CmdArg("-f", "--file", "the (zip) file containing (-d will contain custom extraction folder)", 1);
            HelpArg = new CmdArg("-h", "--help", "Calls your mom");

            HelpArg.SetFinishedParsingCllbk(DoHelp);
        }
        #endregion

        private static List<CmdArg> argList = new List<CmdArg>();
    }
}
