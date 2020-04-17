using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                Console.WriteLine($"Compressing file {file}... Done.");
            }
            
            StreamWriter sw = new StreamWriter(archiveName);
            sw.Write(final);
            sw.Close();
            Console.WriteLine($"Result written to {archiveName}");

        }
        
        public void Decompress()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < 256; i++)
                dictionary.Add(i, ((char)i).ToString());
            
            StreamReader sr = new StreamReader(archiveName);
            int fileCount = 0;
            while (sr.Peek() > -1)
            {
                string file = sr.ReadLine(), decompressed = string.Empty;
                List<string> compressed = file.Split(" ").ToList();
                string filename = compressed[compressed.Count - 1];
                compressed.RemoveAt(compressed.Count - 1);

                string w = dictionary[Convert.ToInt32(compressed[0])];
                compressed.RemoveAt(0);

                foreach (string k in compressed)
                {
                    string entry = null;
                    if (dictionary.ContainsKey(Convert.ToInt32(k)))
                        entry = dictionary[Convert.ToInt32(k)];
                    else if (Convert.ToInt32(k) == dictionary.Count)
                        entry = w + w[0];

                    decompressed += entry;

                    dictionary.Add(dictionary.Count, w + entry[0]);

                    w = entry;
                }

                StreamWriter sw = new StreamWriter(filename);
                sw.Write(decompressed);
                sw.Close();
                Console.WriteLine($"Getting out file {filename}... Done.");
                fileCount++;
            }
            Console.WriteLine($"{fileCount} files written.");
        }
        
    }
}