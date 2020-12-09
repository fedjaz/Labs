using System;
using DataAccessLayer.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converter;

namespace DataAccessLayer
{
    public class DataAccessLayer
    {
        SqlConnection connection;

        IParser Parser;

        public DataAccessLayer(Settings.ConnectionOptions options, IParser parser)
        {
            Parser = parser;
            string connectionString = $"Data Source={options.DataSource}; Database={options.Database}; User={options.User}; Integrated Security={options.IntegratedSecurity}";
            connection = new SqlConnection(connectionString);
            connection.Open();
            //SqlCommand command = new SqlCommand("SELECT TOP(10) * FROM Person.Person", connection);
            //var a = command.ExecuteReader();
            //while(a.Read())
            //{

            //}

        }

        public Person GetPerson(int id)
        {
            SqlCommand command = new SqlCommand("GetPerson", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            return Map<Person>(command.ExecuteReader(), Parser).First();
        }

        public Password GetPassword(int id)
        {
            SqlCommand command = new SqlCommand("GetPassword", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            return Map<Password>(command.ExecuteReader(), Parser).First();
        }

        public EmailAdress GetEmail(int id)
        {
            SqlCommand command = new SqlCommand("GetEmail", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            return Map<EmailAdress>(command.ExecuteReader(), Parser).First();
        }

        public PersonPhone GetPhone(int id)
        {
            SqlCommand command = new SqlCommand("GetPhone", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            return Map<PersonPhone>(command.ExecuteReader(), Parser).First();
        }

        public Address GetAddress(int id)
        {
            SqlCommand command = new SqlCommand("GetAddress", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            return Map<Address>(command.ExecuteReader(), Parser).First();
        }

        public List<T> Map<T>(SqlDataReader reader, IParser parser)
        {
            List<Dictionary<string, object>> parsed = Parse(reader);
            List<T> ans = new List<T>();
            foreach(Dictionary<string, object> dict in parsed)
            {
                ans.Add(parser.Map<T>(dict));
            }
            return ans;
        }

        public List<Dictionary<string, object>> Parse(SqlDataReader reader)
        {
            List<Dictionary<string, object>> ans = new List<Dictionary<string, object>>();
            while(reader.Read())
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                for(int i = 0; i < reader.FieldCount; i++)
                {
                    string name = reader.GetName(i);
                    object val = reader.GetValue(i);
                    dict.Add(name, val);
                }
                ans.Add(dict);
            }
            return ans;
        }
    }
}
