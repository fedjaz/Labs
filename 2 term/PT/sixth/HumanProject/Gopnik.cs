using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanProject
{
    class Gopnik : Human, IFighter
    {
        float Strength { get; set; }
        float AlcoholInBlood { get; set; }

        public Gopnik(float strength, float alcoholInBlood) : base()
        {
            Strength = strength;
            AlcoholInBlood = alcoholInBlood;
        }

        public Gopnik(float strength, float alcoholInBlood,
                      string name, string surname, bool isAlive = true)
                      : base(name, surname, isAlive)
        {
            Strength = strength;
            AlcoholInBlood = alcoholInBlood;
        }

        public Gopnik(float strength, float alcoholInBlood,
                      string name, string surname, DateTime dateOfBirth,
                      Genders gender, string adress = null, bool isAlive = true)
                      : base(name, surname, dateOfBirth, gender,
                             adress, isAlive)

        {
            Strength = strength;
            AlcoholInBlood = alcoholInBlood;
        }

        public float Kick()
        {
            float force = Strength * 0.15f + AlcoholInBlood * 0.1f;
            return force;
        }
    }
}
