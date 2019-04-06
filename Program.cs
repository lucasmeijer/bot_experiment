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
            string token = Environment.GetEnvironmentVariable("GITHUB_TOKEN") ?? "notfound";
            Console.WriteLine("GitHub token first few chars: "+token.Substring(0,3));
            Console.WriteLine($"PullRequest number is {Environment.GetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTNUMBER") ?? "nonumber"}");
            
            var credentials = new InMemoryCredentialStore(new Credentials(token));
            var client = new ObservableGitHubClient(new ProductHeaderValue("ophion"), credentials);
            
            var pr = client.PullRequest.Get("lucasmeijer", "bot_experiment", 1).Subscribe(a => Console.WriteLine($"Title is: {a.Title}"),
                onCompleted: () => Environment.Exit(0)
                );

            Console.ReadLine();
        }
    }
}
