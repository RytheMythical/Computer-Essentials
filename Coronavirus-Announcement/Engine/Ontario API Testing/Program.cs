using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ontario_Coronavirus_API;

namespace Ontario_API_Testing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            foreach (string outbreakHomesandCase in Ontario_Coronavirus.LongTermCare.GetNoOutbreakHomeswithCases())
            {
                Console.WriteLine(outbreakHomesandCase);   
            }
            await Task.Delay(-1);
        }
    }
}
