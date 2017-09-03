using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceRandomizer.FileInteraction
{
    class RandomizerFile
    {
        public RandomizerFile(string fileName)
        {
            _fileName = fileName;
            _fileData = System.IO.File.ReadAllText(fileName);
            for(int i = 0; i < fileBlocks.Length; ++i)
            {
                fileBlocks[i] = new List<string>();
            }
        }

        public string GetFileData() { return _fileData; }

        public void RandomizeFile()
        {

        }

        List<string>[] fileBlocks = new List<string>[100];    //used to async walk the file

        string _fileName;
        string _fileData;
    }
}
