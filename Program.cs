using System;
using System.Linq;
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
            string token = Environment.GetEnvironmentVariable("GITHUB_TOKEN") ?? "nope";
            Console.WriteLine("GitHub token first few chars: "+token.Substring(0,3));
            Console.WriteLine($"PullRequest number is {Environment.GetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTNUMBER") ?? "nonumber"}");


            
            var credentials = new InMemoryCredentialStore(new Credentials(token));
            var client = new ObservableGitHubClient(new ProductHeaderValue("ophion"), credentials);
            
            var pr = client.PullRequest.Get("lucasmeijer", "bot_experiment", 2).Subscribe(async a =>
                {
                    
                    if (a.Mergeable != true)
                        throw new Exception("This PR is not reporting itself as mergable");

                    var mergeCommit = a.MergeCommitSha;
                    var queryCommit = client.Repository.Commit.Get("lucasmeijer", "bot_experiment", mergeCommit).Subscribe(commit =>
                    {
                        var referenceCommit = commit.Parents.First(p => p != a.Head);
                        Console.WriteLine($"##vso[task.setvariable variable=PullRequestBase;isOutput=true]{referenceCommit.Sha}");
                        Console.WriteLine($"##vso[task.setvariable variable=TestMergeCommit;isOutput=true]{commit.Sha}");

                        Console.WriteLine($"referenceCommit is {referenceCommit.Sha}");
                        Console.WriteLine($"testMergeCommit is {commit.Sha}");
                        Console.WriteLine($"Base is {a.Base.Sha}");
                        Console.WriteLine($"Head is {a.Head.Sha}");
                        Console.WriteLine($"Title is: {a.Title}");
                    },
                        
                        onCompleted: () => Environment.Exit(0)
                        );
                    
                    
                },
                onError:(e) =>
                {
                   Console.WriteLine("ERROR!!! " + e);
                   Environment.Exit(1);
                });

            Console.ReadLine();
        }
    }
}
