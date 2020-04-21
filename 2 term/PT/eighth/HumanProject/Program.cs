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
            List<Human> people = new List<Human>()
            {          
                new Runner(9.69f, "Jamaica",
                           50, 97, 99, 95,
                           "Yohan", "Blake"),
                new SoccerPlayer(10, "Barselona",
                                 SoccerPlayer.Positions.Forward,
                                 100, 100, 100, 100,
                                 "Lionel", "Messi"),
                new Runner(9.58f, "Jamaica",
                           50, 100, 100, 100,
                           "Usain", "Bolt"),
                new Boxer(Boxer.Hands.Right,
                          80, 100, 50, 97, 100, 98,
                          "Mike", "Tyson"),
                new Boxer(Boxer.Hands.Right,
                          80, 100, 50, 100, 97, 100,
                          "Muhammad", "Ali"),
                new Gopnik(100, 70, "Vasya", "Ivanov", false)
            };

            RunCompetition(people.Where(runner => runner is IRunner)
                                 .Select(runner => runner as IRunner));

            Console.WriteLine("_________________________");

            FightCompetition(people.Where(fighter => fighter is IFighter)
                                   .Select(fighter => fighter as IFighter));
        }

        static void RunCompetition(IEnumerable<IRunner> runners)
        {
            List<IRunner> participants = runners.ToList();
            participants.Sort();
            foreach(IRunner runner in participants)
            {
                Console.WriteLine(runner.Name + " " + runner.Surname);
                Console.WriteLine("Result: " + runner.Run(100) + " seconds on 100 meter run");
            }
        }

        static void FightCompetition(IEnumerable<IFighter> fighters)
        {
            Random r = new Random();
            for(int i = 0; i < fighters.Count(); i++)
            {
                for(int j = i + 1; j < fighters.Count(); j++)
                {
                    float fighter1 = fighters.ElementAt(i) .Kick();
                    float fighter2 = fighters.ElementAt(j).Kick();
                    float chance1 = fighter1 / (fighter1 + fighter2);
                    float chance2 = fighter2 / (fighter1 + fighter2);
                    Console.WriteLine("{0} {1} vs {2} {3}",
                                      fighters.ElementAt(i).Name, fighters.ElementAt(i).Surname,
                                      fighters.ElementAt(j).Name, fighters.ElementAt(j).Surname);
                    string winnerName = fighters.ElementAt(j).Name, winnerSurname = fighters.ElementAt(j).Surname;
                    float winChance = chance2;
                    if(chance1 > r.NextDouble())
                    {
                        winnerName = fighters.ElementAt(i).Name;
                        winnerSurname = fighters.ElementAt(i).Surname;
                        winChance = chance1;
                    }
                    Console.WriteLine("{0} {1} wins with chance {2}!",
                                          winnerName, winnerSurname,
                                          winChance);
                    Console.WriteLine();
                }
            }
        }
    }
}
