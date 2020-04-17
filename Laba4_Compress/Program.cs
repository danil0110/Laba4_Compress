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

            Archive archive = new Archive(files);
            archive.Decompress();
        }
        
    }
}