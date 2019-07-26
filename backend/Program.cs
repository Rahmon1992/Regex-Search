using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace backend
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Your pattern:");
                //Reads patterns from the console
                string pattern = Console.ReadLine();

                //Path for the folder where the files are
                string docPath = @"C:\Users\Rahmon\Desktop\Example input";

                //Reads all the lines from every file and checks the matching pattern
                var files = from file in Directory.EnumerateFiles(docPath, "*.html", SearchOption.AllDirectories)
                            from line in File.ReadLines(file)
                            where Regex.Match(line, pattern).Success
                            select new
                            {
                                File = file,
                                Id = line.Substring(0,line.IndexOf(":"))
                            };
                //Prints the result
                if (files.Count() !=0)
                {
                    Console.WriteLine("Result:");
                    foreach (var f in files)
                    {
                        Console.WriteLine($"{f.Id}");
                    }
                }
                else
                {
                    Console.WriteLine("No matching");
                }
                
            }
            catch (UnauthorizedAccessException uAEx)
            {
                Console.WriteLine(uAEx.Message);
            }
            catch (PathTooLongException pathEx)
            {
                Console.WriteLine(pathEx.Message);
            }
        }
    }
}
