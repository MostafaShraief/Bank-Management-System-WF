using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DL
{
    internal class clsCurrencies_DL
    {
        private static void FillCommandWithParameters(ref SqlCommand command,
            string CountryName, string CurrencyCode,
            string CurrencyName, float Rate)
        {
            command.Parameters.AddWithValue("@CountryName", CountryName);
            command.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
            command.Parameters.AddWithValue("@CurrencyName", CurrencyName);
            command.Parameters.AddWithValue("@Rate", Rate);
        }

        public static int AddCurrency(string CountryName, string CurrencyCode,
            string CurrencyName, float Rate)
        {
            int CurrencyID = -1;

            SqlConnection connection = clsDLSettings.FillConnection();

            string query = "INSERT INTO [dbo].[Currencies]([CountryName]," +
                " [CurrencyCode], [CurrencyName], [Rate])VALUES(@CountryName," +
                " @CurrencyCode, @CurrencyName, @Rate); Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            FillCommandWithParameters(ref command, CountryName,
                CurrencyCode, CurrencyName, Rate);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                CurrencyID = (result != null &&
                    int.TryParse(result.ToString(), out CurrencyID) ? CurrencyID : -1);
            }
            finally
            {
                connection.Close();
            }

            return CurrencyID;
        }
    }
}
