using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DL
{
    internal class clsDLSettings
    {
        internal static string constr = "Server=.;Database=ContactsDB;User Id=sa;Password=123456;";

        internal static SqlConnection FillConnection()
        {
            SqlConnection connection = new SqlConnection(clsDLSettings.constr);
            return connection;
        }
    }
}
