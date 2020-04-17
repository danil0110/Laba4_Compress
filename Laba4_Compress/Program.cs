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
            files.Add("myArchive.lzw");
            files.Add("simpletext.txt");
            files.Add("rage.txt");
            files.Add("file.txt");
            
            Archive archive = new Archive(files);
            archive.Compress();
        }
        
    }
}