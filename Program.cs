using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RegularExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = File.ReadAllText("TextFile.txt");
            WorkingWithRegularExpressions workingWithRegularExpressions = new WorkingWithRegularExpressions(str);
            //Console.WriteLine(Regular.regular);
            workingWithRegularExpressions.Method();
            Console.Read();
        }
    }
}
