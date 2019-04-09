using System;
using System.IO;

namespace printmessage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("##vso[task.logissue type=warning]BuildSize report has reached a big limit");
            Console.WriteLine("##vso[task.logissue type=error]We found a really big problem");
            Console.WriteLine($"##vso[task.uploadsummary]{Directory.GetCurrentDirectory()}/testsummary.md");
        }
    }
}
