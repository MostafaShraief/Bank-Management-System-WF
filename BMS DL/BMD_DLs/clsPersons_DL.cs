using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DL
{
    public class clsPersons_DL
    {
        private static void FillCommandWithParameters(ref SqlCommand command,
            string FirstName, string LastName, string Phone, string Email)
        {
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
        }

        public static int AddPerson(string FirstName, string LastName, string Phone, string Email)
        {
            int PersonID = -1;

            SqlConnection connection = clsDLSettings.FillConnection();

            string query = "USE [Bank-Management-System] INSERT INTO [dbo].[Persons]" +
                " ([FirstName],[LastName],[Phone],[Email])VALUES(@FirstName,@LastName,@Phone,@Email);" +
                " Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            FillCommandWithParameters(ref command, FirstName,
                LastName, Phone, Email);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                PersonID = (result != null &&
                    int.TryParse(result.ToString(), out PersonID) ? PersonID : -1);
            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }
    }
}
