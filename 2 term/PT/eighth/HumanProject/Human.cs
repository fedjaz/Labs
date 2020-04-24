using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanProject
{
    class Human
    {

        public delegate void PrintMethod(string msg);
        public string Name { get; set; }
        public string Surname { get; set; }
        DateTime dateOfBirth;
        public DateTime DateOfBirth { get => dateOfBirth; set => SetDateOfBirth(value); }
        public int Age { get => dateIsNotNull ? CalculateAge(DateOfBirth) : -1; }
        public bool IsAlive { get; set; }
        public Genders Gender { get; private set; }
        public string Adress { get; set; }
        bool dateIsNotNull = false;

        public Human(bool isAlive = true)
        {

        }

        public Human(string name, string surname, bool isAlive = true)
        {
            Name = name;
            Surname = surname;
            IsAlive = isAlive;
        }

        public Human(string name, string surname, DateTime dateOfBirth, Genders gender, string adress = null, bool isAlive = true)
        {
            Name = name;
            Surname = surname;
            SetDateOfBirth(dateOfBirth);
            Gender = gender;
            IsAlive = isAlive;
            Adress = adress;
        }

        public enum Genders
        {
            Male,
            Female
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine(string.Format("{0} {1}, {2}", Name, Surname, Gender.ToString()));
            if(dateIsNotNull)
            {
                output.Append(string.Format("Date of birth: {0}", DateOfBirth.ToString("dd/MM/yyyy")));
                output.AppendLine(string.Format(" ({0} years)", Age));
            }
            if(Adress != null)
            {
                output.AppendLine(string.Format("Adress: {0}", Adress));
            }
            output.Append(IsAlive ? "Alive" : "Dead");
            return output.ToString();
        }

        public void PrintInformation(PrintMethod print)
        {
            print.Invoke(ToString());
        }

        [Obsolete("No! You shouldn't change your gender!")]
        public void ChangeGender()
        {
            Gender = Gender == Genders.Male ? Genders.Female : Genders.Male;
        }

        [Obsolete("No! You shouldn't change your gender!")]
        public void ChangeGender(Genders gender)
        {
            Gender = gender;
        }

        void SetDateOfBirth(DateTime dateOfBirth)
        {
            this.dateOfBirth = dateOfBirth;
            dateIsNotNull = true;
        }

        public static int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if(DateTime.Now.Month < dateOfBirth.Month ||
              (DateTime.Now.Month == dateOfBirth.Month &&
               DateTime.Now.Day < dateOfBirth.Day))
            {
                age--;                    
            }
            return age;
        }

        public class CompareByAge : IComparer<Human>
        {
            public int Compare(Human h1, Human h2)
            {
                return h2.DateOfBirth.CompareTo(h1.DateOfBirth);
            }
        }

        public class CompareByName : IComparer<Human>
        {
            public int Compare(Human h1, Human h2)
            {
                return h1.Name.CompareTo(h2.Name);
            }
        }

    }
}
