using System;

namespace printmessage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("##vso[task.uploadsummary]testsummary.md");
        }
    }
}
