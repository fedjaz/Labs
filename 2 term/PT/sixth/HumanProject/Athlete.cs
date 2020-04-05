using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanProject
{
    abstract class Athlete : Human
    {
        public float Strength { get; set; }
        public float Agility { get; set; }
        public float Stamina { get; set; }
        public virtual float Skill { get; set; }

        public Athlete(float strength, float agility, float stamina, float skill) : base()
        {
            Strength = strength;
            Agility = agility;
            Stamina = stamina;
            Skill = skill;
        }

        public Athlete(float strength, float agility, float stamina, float skill, 
                       string name, string surname, bool isAlive = true) 
                       : base(name, surname, isAlive)
        {
            Strength = strength;
            Agility = agility;
            Stamina = stamina;
            Skill = skill;
        }

        public Athlete(float strength, float agility, float stamina, float skill,
                       string name, string surname, DateTime dateOfBirth,
                       Genders gender, string adress = null, bool isAlive = true)
                       : base(name, surname, dateOfBirth, gender, 
                              adress, isAlive)
                              
        {
            Strength = strength;
            Agility = agility;
            Stamina = stamina;
            Skill = skill;
        }

        public abstract void Train(int time);

        public override string ToString()
        {
            StringBuilder output = new StringBuilder(base.ToString());
            output.AppendLine();
            output.AppendLine(string.Format("Strength: {0}", Strength));
            output.AppendLine(string.Format("Agility: {0}", Agility));
            output.AppendLine(string.Format("Stamina: {0}", Stamina));
            output.Append(string.Format("Skill: {0}", Skill));
            return output.ToString();
        }
    }
}
