using System;
using DataAccessLayer.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converter;
using System.Transactions;

namespace DataAccessLayer
{
    public class DataAccessLayer
    {
        SqlConnection connection;

        IParser Parser;

        public DataAccessLayer(Settings.ConnectionOptions options, IParser parser)
        {
            Parser = parser;
            string connectionString = $"Data Source={options.DataSource}; Database={options.Database}; User={options.User}; Password={options.Password}; Integrated Security={options.IntegratedSecurity}";
            using(TransactionScope scope = new TransactionScope())
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                scope.Complete();
            }
        }

        public Person GetPerson(int id)
        {
            SqlCommand command = new SqlCommand("GetPerson", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            using(TransactionScope scope = new TransactionScope())
            {
                List<Person> ans = Map<Person>(command.ExecuteReader(), Parser);
                scope.Complete();
                if(ans.Count == 0)
                {
                    return new Person();
                }
                else
                {
                    return ans.First();
                }
            }  
        }

        public int PersonMaxID()
        {
            SqlCommand command = new SqlCommand("GetMaxId", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            using(TransactionScope scope = new TransactionScope())
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int ans = reader.GetInt32(0);
                reader.Close();
                scope.Complete();
                return ans;
            }
        }

        public List<Person> GetPersons()
        {
            SqlCommand command = new SqlCommand("GetPersons", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            using(TransactionScope scope = new TransactionScope())
            {
                List<Person> ans = Map<Person>(command.ExecuteReader(), Parser);
                scope.Complete();
                return ans;
            }
        }

        public List<Person> GetPersonsRange(int startIndex, int count)
        {
            SqlCommand command = new SqlCommand("GetPersonsRange", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("startIndex", startIndex));
            command.Parameters.Add(new SqlParameter("count", count));
            
            using(TransactionScope scope = new TransactionScope())
            {
                scope.Complete();
                List<Person> ans = Map<Person>(command.ExecuteReader(), Parser);
                return ans;
            }
        }

        public Password GetPassword(int id)
        {
            SqlCommand command = new SqlCommand("GetPassword", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            using(TransactionScope scope = new TransactionScope())
            {
                List<Password> ans = Map<Password>(command.ExecuteReader(), Parser);
                scope.Complete();
                if(ans.Count == 0)
                {
                    return new Password();
                }
                else
                {
                    return ans.First();
                }
            }
        }

        public Email GetEmail(int id)
        {
            SqlCommand command = new SqlCommand("GetEmail", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            using(TransactionScope scope = new TransactionScope())
            {
                List<Email> ans = Map<Email>(command.ExecuteReader(), Parser);
                scope.Complete();
                if(ans.Count == 0)
                {
                    return new Email();
                }
                else
                {
                    return ans.First();
                }
            }
        }

        public PersonPhone GetPhone(int id)
        {
            SqlCommand command = new SqlCommand("GetPhone", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            using(TransactionScope scope = new TransactionScope())
            {
                List<PersonPhone> ans = Map<PersonPhone>(command.ExecuteReader(), Parser);
                scope.Complete();
                if(ans.Count == 0)
                {
                    return new PersonPhone();
                }
                else
                {
                    return ans.First();
                }
            }
        }

        public Address GetAddress(int id)
        {
            SqlCommand command = new SqlCommand("GetAddress", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            using(TransactionScope scope = new TransactionScope())
            {
                List<Address> ans = Map<Address>(command.ExecuteReader(), Parser);
                scope.Complete();
                if(ans.Count == 0)
                {
                    return new Address();
                }
                else
                {
                    return ans.First();
                }
            }
        }

        public List<PersonInfo> GetPersonsByJoin()
        {
            List<PersonInfo> ans = new List<PersonInfo>();
            SqlCommand command = new SqlCommand("GetPersonsByJoin", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            using(TransactionScope scope = new TransactionScope())
            {
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    PersonInfo personInfo = new PersonInfo();

                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    for(int i = 0; i < 13; i++)
                    {
                        string name = reader.GetName(i);
                        object val = reader.GetValue(i);
                        dict.Add(name, val);
                    }
                    personInfo.Person = Parser.Map<Person>(dict);

                    dict = new Dictionary<string, object>();
                    for(int i = 13; i < 18; i++)
                    {
                        string name = reader.GetName(i);
                        object val = reader.GetValue(i);
                        dict.Add(name, val);
                    }
                    personInfo.Password = Parser.Map<Password>(dict);

                    dict = new Dictionary<string, object>();
                    for(int i = 18; i < 23; i++)
                    {
                        string name = reader.GetName(i);
                        object val = reader.GetValue(i);
                        dict.Add(name, val);
                    }
                    personInfo.Email = Parser.Map<Email>(dict);

                    dict = new Dictionary<string, object>();
                    for(int i = 23; i < 27; i++)
                    {
                        string name = reader.GetName(i);
                        object val = reader.GetValue(i);
                        dict.Add(name, val);
                    }
                    personInfo.PersonPhone = Parser.Map<PersonPhone>(dict);

                    dict = new Dictionary<string, object>();
                    for(int i = 27; i < 35; i++)
                    {
                        string name = reader.GetName(i);
                        object val = reader.GetValue(i);
                        dict.Add(name, val);
                    }
                    personInfo.Address = Parser.Map<Address>(dict);

                    ans.Add(personInfo);
                }
            }
            return ans;
        }

        List<T> Map<T>(SqlDataReader reader, IParser parser)
        {
            List<Dictionary<string, object>> parsed = Parse(reader);
            List<T> ans = new List<T>();
            foreach(Dictionary<string, object> dict in parsed)
            {
                ans.Add(parser.Map<T>(dict));
            }
            return ans;
        }

        List<Dictionary<string, object>> Parse(SqlDataReader reader)
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
            reader.Close();
            return ans;
        }

        public void Log(DateTime date, string message)
        {
            SqlCommand command = new SqlCommand("Log", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("date", date));
            command.Parameters.Add(new SqlParameter("message", message));
            
            using(TransactionScope scope = new TransactionScope())
            {
                command.ExecuteNonQuery();
                scope.Complete();
            }
        }
    }
}
