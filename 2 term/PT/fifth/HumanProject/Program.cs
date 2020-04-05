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
            List<Athlete> gym = new List<Athlete>()
            {
                new SoccerPlayer(10, "Barselona",
                                 SoccerPlayer.Positions.Forward,
                                 100, 100, 100, 100,
                                 "Lionel", "Messi"),
                new Boxer(Boxer.Hands.Right, 80,
                          100, 50, 100, 100, 100,
                          "Mike", "Tyson"),
                new Runner(9.58f, "Jamaica",
                           50, 100, 100, 100,
                           "Usain", "Bolt")
            };

            foreach(Athlete athlete in gym)
            {
                Console.WriteLine("Before training: ");
                athlete.PrintInformation();
                athlete.Train(1);
                Console.WriteLine("\nAfter: ");
                athlete.PrintInformation();
                Console.WriteLine("__________________________");
            }
        }
    }
}
