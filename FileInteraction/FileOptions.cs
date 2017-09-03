using SourceRandomizer.Utils.CmdLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceRandomizer.FileInteraction
{
    class FileOptions
    {
        public static CmdArg folderLocationArg = new CmdArg("-d", "--directory", "the dir where the sauce is located", 1);
        public static CmdArg fileLocationArg = new CmdArg("-f", "--file", "the (zip) file containing (-d will contain custom extraction folder)", 1);


    }
}
