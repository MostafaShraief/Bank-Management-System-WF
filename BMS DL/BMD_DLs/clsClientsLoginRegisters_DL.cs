using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DL
{
    internal class clsClientsLoginRegisters_DL
    {
        private static void FillCommandWithParameters(ref SqlCommand command,
        int ClientID, DateTime DateTime)
        {
            command.Parameters.AddWithValue("@ClientID", ClientID);
            command.Parameters.AddWithValue("@DateTime", DateTime);
        }

        public static int AddClientLoginRegister(int ClientID, DateTime DateTime)
        {
            int ClientLoginRegisterID = -1;

            // We need to check if the clientid is exist first then we decide if continue or not.

            SqlConnection connection = clsDLSettings.FillConnection();

            string query = "INSERT INTO [dbo].[Clients Login Registers]([ClientID]," +
                "[DateTime])VALUES(@ClientID,@DateTime); Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            FillCommandWithParameters(ref command, ClientID,
                DateTime);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                ClientLoginRegisterID = (result != null &&
                    int.TryParse(result.ToString(), out ClientLoginRegisterID) ? ClientLoginRegisterID : -1);
            }
            finally
            {
                connection.Close();
            }

            return ClientLoginRegisterID;
        }
    }
}
