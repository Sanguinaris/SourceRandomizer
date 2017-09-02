using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceRandomizer.Utils.cmdLine
{
    class CmdParser
    {
        /*  Command Line ARGS:
         * -d --directory   the directory where the source code will be
         * -f --file        the (zip) file containing the source code       (when this is set -d will be the extraction folder)
         * -b --build       cmake | custom  cmake will walk the (first) CMakeLists.txt 
         * ^^^^^^^^^^ will only implement cmake
         * -h --help        will call your mom
         */


        public static void AddCmdArg(CmdArg arg) { argList.Add(arg); }

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

        private static List<CmdArg> argList = new List<CmdArg>();
    }
}
