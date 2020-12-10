using System;
using Converter;
using CommonClasses;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ServiceLayer
    {
        public DataAccessLayer.DataAccessLayer DAL;
        IParser Parser;
        ILogger Logger;
        public ServiceLayer(DataAccessLayer.Settings.ConnectionOptions options, IParser parser, ILogger logger)
        {
            DAL = new DataAccessLayer.DataAccessLayer(options, parser);
            Parser = parser;
            Logger = logger;
        }

        public DataAccessLayer.Models.PersonInfo GetPersonInfo(int id)
        {
            DataAccessLayer.Models.Person person = DAL.GetPerson(id);
            
            DataAccessLayer.Models.PersonInfo personInfo = LoadPerson(person);

            return personInfo;
        }

        public List<DataAccessLayer.Models.PersonInfo> GetPersons()
        {
            List<DataAccessLayer.Models.Person> persons = DAL.GetPersons();
            List<DataAccessLayer.Models.PersonInfo> ans = new List<DataAccessLayer.Models.PersonInfo>();
            foreach(DataAccessLayer.Models.Person person in persons)
            {              
                DataAccessLayer.Models.PersonInfo personInfo = LoadPerson(person);
                ans.Add(personInfo);
            }
            return ans;
        }

        public List<DataAccessLayer.Models.PersonInfo> GetPersonsRange(int startIndex, int count)
        {
            List<DataAccessLayer.Models.Person> persons = DAL.GetPersonsRange(startIndex, count);
            List<DataAccessLayer.Models.PersonInfo> ans = new List<DataAccessLayer.Models.PersonInfo>();

            foreach(DataAccessLayer.Models.Person person in persons)
            {
                DataAccessLayer.Models.PersonInfo personInfo = LoadPerson(person);
                ans.Add(personInfo);
            }
            return ans;
        }

        public List<DataAccessLayer.Models.PersonInfo> GetPersonsByJoin()
        {
            return DAL.GetPersonsByJoin();
        }

        DataAccessLayer.Models.PersonInfo LoadPerson(DataAccessLayer.Models.Person person)
        {
            int id = person.BusinessEntityID;
            DataAccessLayer.Models.Password password = DAL.GetPassword(id);
            DataAccessLayer.Models.Email email = DAL.GetEmail(id);
            DataAccessLayer.Models.PersonPhone personPhone = DAL.GetPhone(id);
            DataAccessLayer.Models.Address address = DAL.GetAddress(id);
            DataAccessLayer.Models.PersonInfo personInfo = new DataAccessLayer.Models.PersonInfo(person, password, email, personPhone, address);
            return personInfo;
        }


    }
}
