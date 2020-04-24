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
            SoccerPlayer Messi = new SoccerPlayer(10, "Barselona",
                                 SoccerPlayer.Positions.Forward,
                                 100, 100, 100, 100,
                                 "Lionel", "Messi");

            SoccerPlayer Ronaldo = new SoccerPlayer(10, "Real Madrid",
                                 SoccerPlayer.Positions.Forward,
                                 100, 100, 100, 100,
                                 "Cristiano", "Ronaldo");

            SoccerPlayer Yashin = new SoccerPlayer(6, "Dynamo",
                                  SoccerPlayer.Positions.Goalkeeper,
                                  100, 100, 100, 100,
                                  "Lev", "Yashin", false);

            Messi.Train(25, TrainWithAnabolics);
            //training with custom function
            Messi.PrintInformation(x => Console.WriteLine(x));
            //passing lambda-expression as parameter

            Console.WriteLine("__________________________");
            try
            {
                Yashin.Train(25);
            }
            catch(InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                Yashin.Run(100);
            }
            catch(InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            //handling exceptions
            Console.WriteLine("__________________________");
            Messi.Celebrate += (message) => Console.WriteLine(message);
            Messi.GetUpset += (message) => Console.WriteLine(message);
            Ronaldo.Celebrate += delegate (string message) { Console.WriteLine(message); };
            Ronaldo.GetUpset += delegate (string message) { Console.WriteLine(message); };
            SoccerGame FCBvsRMA = new SoccerGame(new List<SoccerPlayer>() { Messi },
                                                 new List<SoccerPlayer>() { Ronaldo });
            FCBvsRMA.Penalty();
            //testing events
        }

        static void TrainWithAnabolics(ref float skill, ref float strength, ref float stamina, ref float agility, int time)
        {
            skill += time * 0.25f;
            strength += time * 0.025f;
            stamina += time * 0.4f;
            agility += time * 0.3f;
        }
    }
}
