using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Athlete> runners = new List<Athlete>()
            {
                new Runner(9.58f, "Jamaica",
                           50, 100, 100, 100,
                           "Usain", "Bolt"),
                new Runner(9.69f, "Jamaica",
                           50, 97, 99, 95,
                           "Yohan", "Blake"),
                new SoccerPlayer(10, "Barselona",
                                 SoccerPlayer.Positions.Forward,
                                 100, 100, 100, 100,
                                 "Lionel", "Messi")
            };

            foreach(Athlete participant in runners)
            {
                if(participant is IRunner)
                {
                    Run(participant as IRunner);
                }
            }
        }

        static void Run(IRunner runner)
        {
            Console.WriteLine(runner.Name + " " + runner.Surname);
            Console.WriteLine("Result: " + runner.Run(100) + " seconds on 100 meter run");
        }
    }
}
