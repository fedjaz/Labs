using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanProject
{
    class SoccerPlayer : Athlete
    {
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

        public override void Train(int time)
        {
            Skill += time * 0.1f;
            Strength += time * 0.01f;
            Stamina += time * 0.2f;
            Agility += time * 0.15f;
        }
    }
}
