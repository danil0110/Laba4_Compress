using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
 
namespace Laba4_Compress
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<string> files = new List<string>();
            if (args.Length > 0)
            {
                foreach (var el in args)
                {
                    files.Add(el);
                }
            }

            string key = files[0];
            files.RemoveAt(0);

            Archive LZW = new Archive(files);
            
            if (key == "--compress")
                LZW.Compress();
            else if (key == "--decompress")
                LZW.Decompress();
            else
            {
                Console.WriteLine("Error: wrong input.");
            }
            
        }
        
    }
}