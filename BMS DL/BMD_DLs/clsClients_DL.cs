using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DL
{
    internal class clsClients_DL
    {
        private static void FillCommandWithParameters(ref SqlCommand command,int PersonID, string PINCode, float Balance)
        {
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@PINCode", PINCode);
            command.Parameters.AddWithValue("@Balance", Balance);
        }

        public static int AddClient(string FirstName, string LastName, string Phone,
            string Email, string PINCode, float Balance)
        {
            int ClientID = -1;

            SqlConnection connection = clsDLSettings.FillConnection();

            int PersonID = clsPersons_DL.AddPerson(FirstName, LastName, Phone, Email);

            if (PersonID == -1)
                return ClientID; // Failed To Add Person To The Table.

            string query = "INSERT INTO [dbo].[Clients]([PersonID],[PINCode]," +
                "[Balance])VALUES(@PersonID, @PINCode,@Balance); Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            FillCommandWithParameters(ref command, PersonID, PINCode, Balance);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                ClientID = (result != null &&
                    int.TryParse(result.ToString(), out ClientID) ? ClientID : -1);
            }
            finally
            {
                connection.Close();
            }

            return ClientID;
        }

        // we need function to add client with personid only too.
    }
}
