using ABCMoneyTransfer.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ABCMoneyTransfer.Services
{
    public class TransactionService(IConfiguration configuration)
    {
        //private readonly AbcremittanceDbContext _abcremittanceDbContext;
        //public TransactionService(AbcremittanceDbContext abcremittanceDbContext)
        //{
        //    _abcremittanceDbContext = abcremittanceDbContext;
        //}

        private readonly string _connectionString = configuration.GetConnectionString("ABCRemittanceDB");

        public async Task<(int ReturnCode, string ReturnStatus)> AddTransactionAsync(
            string senderName,
            string receiverName,
            string senderAddress,
            string receiverAddress,
            string bankName,
            string accountNumber,
            decimal transferAmount,
            decimal payoutAmount,
            string currency,
            string senderCountry,
            string receiverCountry,
            string status,
             string senderIdentity,   
            string receiverIdentity, 
            string senderMobile,     
            string receiverMobile,   
            decimal exchangeRate,    
            decimal serviceCharge    
        )
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("Proc_Transaction", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Input parameters
                command.Parameters.AddWithValue("@Flag", "i");
                command.Parameters.AddWithValue("@SenderName", senderName);
                command.Parameters.AddWithValue("@ReceiverName", receiverName);
                command.Parameters.AddWithValue("@SenderAddress", senderAddress);
                command.Parameters.AddWithValue("@ReceiverAddress", receiverAddress);
                command.Parameters.AddWithValue("@BankName", bankName);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                command.Parameters.AddWithValue("@TransferAmount", transferAmount);
                command.Parameters.AddWithValue("@PayoutAmount", payoutAmount);
                command.Parameters.AddWithValue("@Currency", currency);
                command.Parameters.AddWithValue("@SenderCountry", senderCountry);
                command.Parameters.AddWithValue("@ReceiverCountry", receiverCountry);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@SIdentity", senderIdentity);
                command.Parameters.AddWithValue("@RIdentity", receiverIdentity);
                command.Parameters.AddWithValue("@SMobile", senderMobile);
                command.Parameters.AddWithValue("@RMobile", receiverMobile);
                command.Parameters.AddWithValue("@ExchangeRate", exchangeRate);
                command.Parameters.AddWithValue("@ServiceCharge", serviceCharge);

                // Output parameters
                var returnCodeParam = new SqlParameter("@ReturnCode", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(returnCodeParam);

                var returnStatusParam = new SqlParameter("@ReturnStatus", SqlDbType.NVarChar, 50)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(returnStatusParam);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                // Retrieve output values
                var returnCode = (int)returnCodeParam.Value;
                var returnStatus = (string)returnStatusParam.Value;

                return (returnCode, returnStatus);
            }
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            var transactions = new List<Transaction>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("Proc_Transaction", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Flag", "s");

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        transactions.Add(new Transaction
                        {
                            Id = reader.GetInt32("Id"),
                            SenderName = reader.GetString("SenderName"),
                            ReceiverName = reader.GetString("ReceiverName"),
                            SenderAddress = reader.GetString("SenderAddress"),
                            ReceiverAddress = reader.GetString("ReceiverAddress"),
                            BankName = reader.GetString("BankName"),
                            AccountNumber = reader.GetString("AccountNumber"),
                            TransferAmount = reader.GetDecimal("TransferAmount"),
                            PayoutAmount = reader.GetDecimal("PayoutAmount"),
                            Currency = reader.GetString("Currency"),
                            SenderCountry = reader.GetString("SenderCountry"),
                            ReceiverCountry = reader.GetString("ReceiverCountry"),
                            Status = reader.GetString("Status")
                        });
                    }
                }
            }

            return transactions;
        }
    }

}

