using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DL
{
    internal class clsPermissions_DL
    {
        private static void FillCommandWithParameters(ref SqlCommand command,
            string PermissionName, int PermissionValue)
        {
            command.Parameters.AddWithValue("@PermissionName", PermissionName);
            command.Parameters.AddWithValue("@PermissionValue", PermissionValue);
        }

        public static int AddPermission(string PermissionName, int PermissionValue)
        {
            int PermissionID = -1;

            SqlConnection connection = clsDLSettings.FillConnection();

            string query = "INSERT INTO [dbo].[Permissions]([PermissionName]," +
                "[PermissionValue])VALUES" +
                "(@PermissionName,@PermissionValue); Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            FillCommandWithParameters(ref command, PermissionName,
                PermissionValue);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                PermissionID = (result != null &&
                    int.TryParse(result.ToString(), out PermissionID) ? PermissionID : -1);
            }
            finally
            {
                connection.Close();
            }

            return PermissionID;
        }
    }
}
