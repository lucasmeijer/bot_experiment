using System;
using System.Security.Principal;
using Octokit;
using Octokit.Internal;
using Octokit.Reactive;

namespace bot_experiment
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = Environment.GetEnvironmentVariable("GITHUB_TOKEN") ?? "none";
            Console.WriteLine("GitHub token first few chars: "+token.Substring(0,3));
            Console.WriteLine($"PullRequest number is {Environment.GetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTNUMBER") ?? "nonumber"}");
            
            var credentials = new InMemoryCredentialStore(new Credentials(token));
            var client = new ObservableGitHubClient(new ProductHeaderValue("ophion"), credentials);
            
            var pr = client.PullRequest.Get("lucasmeijer", "bot_experiment", 1).Subscribe(a =>
                {
                    Console.WriteLine($"##vso[task.setvariable variable=PullRequestBase;isOutput=true]{a.Base.Sha}");
                    Console.WriteLine($"Base is {a.Base.Sha}");
                    Console.WriteLine($"Head is {a.Head.Sha}");
                    
                    
                    
                    Console.WriteLine($"Title is: {a.Title}");
                },
                onCompleted: () => Environment.Exit(0),
                onError:(e) =>
                {
                   Console.WriteLine("ERROR!!! " + e);
                   Environment.Exit(1);
                });

            Console.ReadLine();
        }
    }
}
