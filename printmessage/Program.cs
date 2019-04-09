using System;
using System.IO;

namespace printmessage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"##vso[task.uploadsummary]{Directory.GetCurrentDirectory()}/testsummary.md");
        }
    }
}
