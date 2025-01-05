using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DL
{
    internal class clsUsers_DL
    {
        private static void FillCommandWithParameters(ref SqlCommand command,
            int PersonID, string UserName, string Password, int Permissions)
        {
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@Permissions", Permissions);
        }

        public static int AddUser(string FirstName, string LastName, string Phone,
            string Email, string UserName, string Password, int Permissions)
        {
            int UserID = -1;

            SqlConnection connection = clsDLSettings.FillConnection();

            int PersonID = clsPersons_DL.AddPerson(FirstName, LastName, Phone, Email);

            if (PersonID == -1)
                return UserID; // Failed To Add Person To The Table.

            string query = "INSERT INTO [dbo].[Users]([UserID],[PersonID],[UserName]," +
                "[Password],[Permissions])VALUES(@UserID,@PersonID,@UserName,@Password," +
                "@Permissions); Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            FillCommandWithParameters(ref command, PersonID, UserName, Password, Permissions);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                UserID = (result != null &&
                    int.TryParse(result.ToString(), out UserID) ? UserID : -1);
            }
            finally
            {
                connection.Close();
            }

            return UserID;
        }

        // we need function to add user with personid only too.
    }
}
