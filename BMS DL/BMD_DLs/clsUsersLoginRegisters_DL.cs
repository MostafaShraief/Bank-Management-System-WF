using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DL
{
    internal class clsUsersLoginRegisters_DL
    {
        private static void FillCommandWithParameters(ref SqlCommand command,
        int UserID, DateTime DateTime)
        {
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@DateTime", DateTime);
        }

        public static int AddUserLoginRegister(int UserID, DateTime DateTime)
        {
            int UserLoginRegisterID = -1;

            // We need to check if the userid is exist first then we decide if continue or not.

            SqlConnection connection = clsDLSettings.FillConnection();

            string query = "INSERT INTO [dbo].[Users Login Registers]([UserID],[DateTime])" +
                "VALUES(@UserID,@DateTime); Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            FillCommandWithParameters(ref command, UserID,
                DateTime);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                UserLoginRegisterID = (result != null &&
                    int.TryParse(result.ToString(), out UserLoginRegisterID) ? UserLoginRegisterID : -1);
            }
            finally
            {
                connection.Close();
            }

            return UserLoginRegisterID;
        }
    }
}
