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

        public DataAccessLayer(Settings.ConnectionOptions options)
        {
            string connectionString = $"Data Source={options.DataSource}; Database={options.Database}; User={options.User}; Integrated Security={options.IntegratedSecurity}";
            connection = new SqlConnection(connectionString);
            connection.Open();
            //SqlCommand command = new SqlCommand("SELECT TOP(10) * FROM Person.Person", connection);
            //var a = command.ExecuteReader();
            //while(a.Read())
            //{

            //}

        }
    }
}
