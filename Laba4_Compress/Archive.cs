using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;

namespace Laba4_Compress
{
    public class Archive
    {
        private List<string> files;
        private static string archiveName;

        public Archive(List<string> inputData)
        {
            archiveName = inputData[0];
            inputData.RemoveAt(0);
            if (inputData.Count != 0)
                files = inputData;
        }

        public void Compress()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int i = 0; i < 256; i++)
                dictionary.Add(((char)i).ToString(), i);
            
            string final = string.Empty;
            StreamReader sr;

            foreach (var file in files)
            {
                sr = new StreamReader(file);
                string uncompressed = sr.ReadToEnd(), w = string.Empty;
                List<int> compressed = new List<int>();

                foreach (char c in uncompressed)
                {
                    string wc = w + c;
                    if (dictionary.ContainsKey(wc))
                    {
                        w = wc;
                    }
                    else
                    {
                        compressed.Add(dictionary[w]);
                        dictionary.Add(wc, dictionary.Count);
                        w = c.ToString();
                    }
                }

                if (!string.IsNullOrEmpty(w))
                    compressed.Add(dictionary[w]);
                
                sr.Close();

                final += String.Join(' ', compressed) + $" {file}" +'\n';
            }
            
            StreamWriter sw = new StreamWriter(archiveName);
            sw.Write(final);
            sw.Close();

        }
    }
}