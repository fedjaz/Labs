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
        DataAccessLayer.DataAccessLayer DAL;
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
            DataAccessLayer.Models.Password password = DAL.GetPassword(id);
            DataAccessLayer.Models.Email email = DAL.GetEmail(id);
            DataAccessLayer.Models.PersonPhone personPhone = DAL.GetPhone(id);
            DataAccessLayer.Models.Address address = DAL.GetAddress(id);
            DataAccessLayer.Models.PersonInfo personInfo = new DataAccessLayer.Models.PersonInfo(person, password, email, personPhone, address);

            return personInfo;
        }
    }
}
