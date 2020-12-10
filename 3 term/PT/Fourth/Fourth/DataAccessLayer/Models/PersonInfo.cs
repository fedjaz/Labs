using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class PersonInfo
    { 
        public Person Person { get; set; }
        public Password Password { get; set; }
        public Email Email { get; set; }
        public PersonPhone PersonPhone { get; set; }
        public Address Address { get; set; }

        public PersonInfo(Person person, Password password, Email email, PersonPhone personPhone, Address address)
        {
            Person = person;
            Password = password;
            Email = email;
            PersonPhone = personPhone;
            Address = address;
        }

        public PersonInfo()
        {

        }
    }
}
