using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_DL
{
    internal class clsTransferRegisters_DL
    {
        private static void FillCommandWithParameters(ref SqlCommand command,
        int SenderClientID, int ReciverClientID, DateTime DateTime, float Amount,
        float SenderBalance, float ReciverBalance, int UserID)
        {
            command.Parameters.AddWithValue("@SenderClientID", SenderClientID);
            command.Parameters.AddWithValue("@ReciverClientID", ReciverClientID);
            command.Parameters.AddWithValue("@DateTime", DateTime);
            command.Parameters.AddWithValue("@Amount", Amount);
            command.Parameters.AddWithValue("@SenderBalance", SenderBalance);
            command.Parameters.AddWithValue("@ReciverBalance", ReciverBalance);

            if (UserID != -1)
                command.Parameters.AddWithValue("@UserID", UserID);
            else
                command.Parameters.AddWithValue("@UserID", DBNull.Value);
        }

        public static int AddTransferRegister(int SenderClientID, int ReciverClientID,
            DateTime DateTime, float Amount, float SenderBalance, float ReciverBalance, int UserID = -1)
        {
            int TransferRegistersID = -1;

            // We need to check if the s&r client are exist and if userid is sent we need to check its too.

            SqlConnection connection = clsDLSettings.FillConnection();

            string query = "INSERT INTO [dbo].[Transfer Registers]([SenderClientID]," +
                "[ReciverClientID],[UserID],[DateTime],[Amount],[SenderBalance],[ReciverBalance])" +
                "VALUES(@SenderClientID,@ReciverClientID,@UserID,@DateTime,@Amount,@SenderBalance," +
                "@ReciverBalance); Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            FillCommandWithParameters(ref command, SenderClientID,
                ReciverClientID, DateTime, Amount, SenderBalance, ReciverBalance, UserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                TransferRegistersID = (result != null &&
                    int.TryParse(result.ToString(), out TransferRegistersID) ? TransferRegistersID : -1);
            }
            finally
            {
                connection.Close();
            }

            return TransferRegistersID;
        }
    }
}
