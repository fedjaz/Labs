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
            Console.OutputEncoding = Encoding.UTF8;
            List<Human> humen = new List<Human>();
            humen.Add(new Human("Grzegorz", "Brzęczyszczykiewicz", new DateTime(1925, 3, 28), Human.Genders.Male,
                "Chrząszczyżewoszyce, powiat Łękołody, Polska"));
            while(true)
            {
                Console.WriteLine("1.Print all database\n" +
                                  "2.Add new human\n" +
                                  "3.Delete human by index\n" +
                                  "4.Sort by name\n" + 
                                  "5.Sort by age\n" + 
                                  "6.Exit");

                int response;
                if(!int.TryParse(Console.ReadLine(), out response) || response < 1 ||
                    response > 6)
                {
                    Console.WriteLine("Wrong input, try again");
                    continue;
                }
                switch (response)
                {
                    case 1:
                        {
                            humen.ForEach(human => human.PrintInformation());
                            break;
                        }
                        
                    case 2:
                        {
                            Human human = GetHuman();
                            if(human == null)
                            {
                                Console.WriteLine("Wrong input, try again");
                                continue;
                            }
                            humen.Add(human);
                            break;
                        }
                    case 3:
                        {
                            if (!DeleteByIndex(humen))
                            {
                                Console.WriteLine("Wrong input, try again");
                                continue;
                            }
                            break;
                        }
                    case 4:
                        {
                            humen.Sort(new Human.CompareByName());
                            break;
                        }
                    case 5:
                        {
                            humen.Sort(new Human.CompareByAge());
                            break;
                        }
                    case 6:
                        {
                            return;
                        }
                }
            }
        }
        static bool DeleteByIndex<T>(List<T> list)
        {
            try
            {
                Console.WriteLine("Write index of element to delete: ");
                int index = int.Parse(Console.ReadLine().Trim().Split(' ')[0]);
                if(index < 0 || index > list.Count() - 1)
                {
                    return false;
                }
                list.RemoveAt(index);
                return true;
            }
            catch
            {
                return false;
            }
        }
        static Human GetHuman()
        {
            try
            {
                string name, surname;
                Console.WriteLine("Write name and surname through space: ");
                string[] nameArr = Console.ReadLine().Split(' ').Where(x => x != "").ToArray();
                (name, surname) = (nameArr[0], nameArr[1]);
                Console.WriteLine("Write date of birth (3 numbers through space): ");
                int[] dateArr = Console.ReadLine().Split(' ').Where(x => x != "").Select(x => int.Parse(x)).ToArray();
                DateTime dateOfBirth = new DateTime(dateArr[2], dateArr[1], dateArr[0]);
                Console.WriteLine("Write adress in 1 line:");
                string adress = Console.ReadLine();
                Console.WriteLine("Type y if person is alive: ");
                bool isAlive = Console.ReadLine().Trim().ToLower()[0] == 'y';
                Console.WriteLine("Type y if person is male: ");
                bool isMale = Console.ReadLine().Trim().ToLower()[0] == 'y';
                Human.Genders gender = isMale ? Human.Genders.Male : Human.Genders.Female;
                return new Human(name, surname, dateOfBirth, gender, adress, isAlive);
            }
            catch
            {
                return null;
            }
        }
    }
}
