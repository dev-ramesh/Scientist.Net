using GitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientist_Net
{
    class ConsolePublisher : IResultPublisher
    {
        public Task Publish<T, TClean>(Result<T, TClean> result)
        {

            Console.ForegroundColor = result.Mismatched ? ConsoleColor.Red : ConsoleColor.Green;


            Console.WriteLine($"Experiment Name '{result.ExperimentName}'");
            Console.WriteLine($"Result: {(result.Matched?"Control value matched ":"Control value not matched")}");
            Console.WriteLine($"Control value: {result.Control.Value}");

            foreach (var candidate in result.Candidates)
            {
                Console.WriteLine($"Candidate name :{candidate.Name}");
                Console.WriteLine($"Candidate Value :{candidate.Value}");
            }


            Console.ForegroundColor = ConsoleColor.White;

            return Task.FromResult(0);

        }
    }
}
