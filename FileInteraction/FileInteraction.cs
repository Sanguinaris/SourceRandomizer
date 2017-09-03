using SourceRandomizer.Utils.CmdLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SourceRandomizer.FileInteraction
{
    class FileInteraction
    {
        public static void DoMutate()
        {
            if (!CmdParser.folderLocationArg.IsParsed())
            {
                Console.WriteLine("[DoMutate] Folder Location wasnt Parsed...");
                return;
            }
            string folderLocation = CmdParser.folderLocationArg.GetAdditionalArg(0);

            if (Directory.Exists(folderLocation + "/rnd"))
            {
                Directory.Delete(folderLocation + "/rnd");
            }
            Directory.CreateDirectory(folderLocation + "/rnd");

            foreach (string file in Directory.EnumerateFiles(folderLocation, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".cpp") || s.EndsWith(".h") || s.EndsWith(".hpp")))
            {
                try
                {
                    RandomizerFile rndFile = new RandomizerFile(file);
                    rndFile.RandomizeFile();
                    string outFile = file.Insert(folderLocation.Length, "/rnd");
                    if (!Directory.Exists(Path.GetDirectoryName(outFile)))
                        Directory.CreateDirectory(Path.GetDirectoryName(outFile));
                    File.WriteAllText(outFile, rndFile.GetFileData());
                }
                catch (Exception e)
                {
                    Console.WriteLine("[DoMutate] Exception in Randomization (" + file + ") Loop: " + e.Message);
                }
            }
            foreach (string file in Directory.EnumerateFiles(folderLocation, "CMakeLists.txt", SearchOption.AllDirectories))
            {
                try
                {
                    string outFile = file.Insert(folderLocation.Length, "/rnd");
                    if (!Directory.Exists(Path.GetDirectoryName(outFile)))
                        Directory.CreateDirectory(Path.GetDirectoryName(outFile));
                    File.Copy(file, outFile);
                }
                catch (Exception e)
                {
                    Console.WriteLine("[DoMutate] Exception in Copying (" + file + "): " + e.Message);
                }
            }
        }
    }
}
