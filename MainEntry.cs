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

        /*  Command Line ARGS:
         * -d --directory   the directory where the source code will be
         * -f --file        the (zip) file containing the source code       (when this is set -d will be the extraction folder)
         * -b --build       cmake | custom  cmake will walk the (first) CMakeLists.txt 
         * ^^^^^^^^^^ will only implement cmake
         * -h --help        will call your mom
         */

        static void Main(string[] args) //ARGS ARE: 
        {
            if (!CmdParser.ParseArgs(args))
                return;

            //DO SHIT
        }
    }
}
