using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanProject
{
    class Runner : Athlete
    {
        public float PersonalRecord { get; set; }
        public string CountryName { get; set; }

        public Runner(float personalRecord, string countryName,
                            float strength, float agility, float stamina,
                            float skill)
                            : base(strength, agility, stamina, skill)
        {
            PersonalRecord = personalRecord;
            CountryName = countryName;
        }

        public Runner(float personalRecord, string countryName,
                            float strength, float agility, float stamina,
                            float skill, string name, string surname,
                            bool isAlive = true)
                            : base(strength, agility, stamina, skill,
                                   name, surname, isAlive)
        {
            PersonalRecord = personalRecord;
            CountryName = countryName;
        }

        public Runner(float personalRecord, string countryName,
                            float strength, float agility, float stamina,
                            float skill, string name, string surname,
                            DateTime dateOfBirth, Genders gender,
                            string adress = null, bool isAlive = true)
                            : base(strength, agility, stamina, skill,
                                   name, surname, dateOfBirth, gender,
                                   adress, isAlive)
        {
            PersonalRecord = personalRecord;
            CountryName = countryName;
        }

        public override void Train(int time)
        {
            Skill += time * 0.2f;
            Stamina += time * 0.2f;
            Agility += time * 0.2f;
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder(base.ToString());
            output.AppendLine();
            output.AppendLine(string.Format("Personal record: {0} seconds", PersonalRecord));
            output.Append(string.Format("Country: {0}", CountryName));
            return output.ToString();
        }
    }
}
