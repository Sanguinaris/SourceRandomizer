using SourceRandomizer.Utils.cmdLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceRandomizer
{
    class MainEntry
    {
        static void Main(string[] args) //ARGS ARE: 
        {
            CmdParser.AddCmdArg(new CmdArg("-d", "--directory", "the dir where the sauce is located", 1));

            if (!CmdParser.ParseArgs(args))
                return;

            //DO SHIT
        }
    }
}
