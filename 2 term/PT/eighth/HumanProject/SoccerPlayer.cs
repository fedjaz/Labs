using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanProject
{
    class SoccerPlayer : Athlete, IRunner
    {
        public delegate void TrainFunction(ref float skill, ref float strength,
                                           ref float stamina, ref float agility, int time);
        public delegate void Reaction(string message);

        public event Reaction Celebrate;
        public event Reaction GetUpset;
        public int Number { get; set; }
        public string TeamName { get; set; }
        public enum Positions
        {
            Goalkeeper,
            Defender,
            Midfielder,
            Forward
        }
        public Positions Position { get; set; }

        public SoccerPlayer(int number, string teamName, Positions position,
                            float strength, float agility, float stamina,
                            float skill)
                            : base(strength, agility, stamina, skill)
        {
            Number = number;
            TeamName = teamName;
            Position = position;
        }

        public SoccerPlayer(int number, string teamName, Positions position,
                            float strength, float agility, float stamina,
                            float skill, string name, string surname, 
                            bool isAlive = true)
                            : base(strength, agility, stamina, skill, 
                                   name, surname, isAlive)
        {
            Number = number;
            TeamName = teamName;
            Position = position;
        }

        public SoccerPlayer(int number, string teamName, Positions position,
                            float strength, float agility, float stamina,
                            float skill, string name, string surname,
                            DateTime dateOfBirth, Genders gender,
                            string adress = null, bool isAlive = true)
                            : base(strength, agility, stamina, skill,
                                   name, surname, dateOfBirth, gender,
                                   adress, isAlive)
        {
            Number = number;
            TeamName = teamName;
            Position = position;
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder(base.ToString());
            output.AppendLine();
            output.AppendLine(string.Format("Team: {0}", TeamName));
            output.AppendLine(string.Format("Number: {0}", Number));
            output.Append(string.Format("Position: {0}", Position.ToString()));
            return output.ToString();
        }

        public float Run(float distance)
        {
            if(!IsAlive)
            {
                throw new InvalidOperationException("This runner is dead.");
            }

            float meanSpeed = Agility * 0.1f + Stamina * 0.1f +
                              Strength * 0.025f;

            return distance / (meanSpeed / 3.6f);
        }

        public override void Train(int time)
        {
            Train(time, DefaultTrainFunction);
        }

        public void Train(int time, TrainFunction function)
        {
            if(!IsAlive)
            {
                throw new InvalidOperationException("This athlete can't train because he is dead.");
            }

            float strength = Strength;
            float skill = Skill;
            float stamina = Stamina;
            float agility = Agility;
            function.Invoke(ref skill, ref strength, ref stamina, ref agility, time);
            (Skill, Strength, Stamina, Agility) = (skill, strength, stamina, agility);
        }

        public void Goal(string teamName)
        {
            if(teamName == TeamName)
            {
                Celebrate?.Invoke($"{Name} {Surname} from {TeamName} is very happy!");
            }
            else
            {
                GetUpset?.Invoke($"{Name} {Surname} from {TeamName} is now upset...");
            }
        }

        public int CompareTo(IRunner runner)
        {
            float time1 = this.Run(100);
            float time2 = runner.Run(100);
            if(time1 > time2)
                return 1;
            else if(time1 < time2)
                return -1;
            return 0;
        }
        void DefaultTrainFunction(ref float skill, ref float strength, ref float stamina, ref float agility, int time)
        {
            skill += time * 0.1f;
            strength += time * 0.01f;
            stamina += time * 0.2f;
            agility += time * 0.15f;
        }
    }
}
