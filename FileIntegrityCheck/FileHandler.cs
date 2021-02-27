using System;
using System.Collections.Generic;
using System.IO;

namespace FileIntegrityCheck
{
    public class FileHandler
    {
        public List<string[]> Read(string path)
        {
            using (var sr = new StreamReader(path))
            {
                string line;

                var output = new List<string[]>();

                while ((line = sr.ReadLine()) != null)
                {
                     output.Add(line.Split(' ',StringSplitOptions.RemoveEmptyEntries));
                }

                return output;
            }
        }
    }
}