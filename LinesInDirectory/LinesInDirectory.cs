using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LinesInDirectory
{
    public class Count
    {
        public int LineCountInFile(string filePath)
        {
            int LinesOfCode = 0;
            List<string> stringList = File.ReadLines(filePath).ToList<string>();
            foreach(string s in stringList)
            {
                if (s != "") LinesOfCode++;
            }
            return LinesOfCode;
        }

        public int LineCountInDirectory(string directoryPath, string searchPattern = "*")
        {
            int numberOfLines = 0;
            FileChecker fileChecker = new FileChecker(); 
            List<string> FilePaths = fileChecker.GetFileNames(directoryPath, searchPattern); //get all file paths.
            foreach (string filePath in FilePaths) //get number of lines per file, then add them up.
            {
                numberOfLines += LineCountInFile(filePath);
            }
            return numberOfLines;
        }
    }

    public class FileChecker
    { 
        public List<string> GetFileNames(string DirectoryPath, string searchPattern)
        {
            List<string> FileNamesList = Directory.EnumerateFiles(DirectoryPath, searchPattern, SearchOption.AllDirectories).ToList<string>();
            return FileNamesList;
        }
    }

    public class Test
    {
        static void Main()
        {
            Test test = new Test(); Count count = new Count();
            Console.WriteLine("Enter Directory Path");
            string directoryPath = Console.ReadLine();
            Console.WriteLine("Enter search pattern. E.g. *.txt");
            string searchPattern = Console.ReadLine();
            if (searchPattern == "") searchPattern = searchPattern + "*.cs";
            Console.WriteLine(count.LineCountInDirectory(directoryPath, searchPattern));
        }

    }
   

}


