using ABCMoneyTransfer.Model;
using ABCMoneyTransfer.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ABCMoneyTransfer.Services
{
    public class TransactionService(AbcremittanceDbContext context)
    {
        //private readonly AbcremittanceDbContext _abcremittanceDbContext;
        //public TransactionService(AbcremittanceDbContext abcremittanceDbContext)
        //{
        //    _abcremittanceDbContext = abcremittanceDbContext;
        //}

        private readonly AbcremittanceDbContext _context = context;


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
            try
            {
                var sql = @"
            EXEC Proc_Transaction 
                @Flag = @Flag, 
                @SenderName = @SenderName, 
                @ReceiverName = @ReceiverName, 
                @SenderAddress = @SenderAddress, 
                @ReceiverAddress = @ReceiverAddress, 
                @BankName = @BankName, 
                @AccountNumber = @AccountNumber, 
                @TransferAmount = @TransferAmount, 
                @PayoutAmount = @PayoutAmount, 
                @Currency = @Currency, 
                @SenderCountry = @SenderCountry, 
                @ReceiverCountry = @ReceiverCountry, 
                @Status = @Status, 
                @SIdentity = @SIdentity, 
                @RIdentity = @RIdentity, 
                @SMobile = @SMobile, 
                @RMobile = @RMobile, 
                @ExchangeRate = @ExchangeRate, 
                @ServiceCharge = @ServiceCharge, 
                @ReturnCode = @ReturnCode OUTPUT, 
                @ReturnStatus = @ReturnStatus OUTPUT";

                var parameters = new[]
                {
            new SqlParameter("@Flag", "i"),
            new SqlParameter("@SenderName", senderName ?? (object)DBNull.Value),
            new SqlParameter("@ReceiverName", receiverName ?? (object)DBNull.Value),
            new SqlParameter("@SenderAddress", senderAddress ?? (object)DBNull.Value),
            new SqlParameter("@ReceiverAddress", receiverAddress ?? (object)DBNull.Value),
            new SqlParameter("@BankName", bankName ?? (object)DBNull.Value),
            new SqlParameter("@AccountNumber", accountNumber ?? (object)DBNull.Value),
            new SqlParameter("@TransferAmount", transferAmount),
            new SqlParameter("@PayoutAmount", payoutAmount),
            new SqlParameter("@Currency", currency ?? (object)DBNull.Value),
            new SqlParameter("@SenderCountry", senderCountry ?? (object)DBNull.Value),
            new SqlParameter("@ReceiverCountry", receiverCountry ?? (object)DBNull.Value),
            new SqlParameter("@Status", status ?? (object)DBNull.Value),
            new SqlParameter("@SIdentity", senderIdentity ?? (object)DBNull.Value),
            new SqlParameter("@RIdentity", receiverIdentity ?? (object)DBNull.Value),
            new SqlParameter("@SMobile", senderMobile ?? (object)DBNull.Value),
            new SqlParameter("@RMobile", receiverMobile ?? (object)DBNull.Value),
            new SqlParameter("@ExchangeRate", exchangeRate),
            new SqlParameter("@ServiceCharge", serviceCharge),
            new SqlParameter("@ReturnCode", SqlDbType.Int) { Direction = ParameterDirection.Output },
            new SqlParameter("@ReturnStatus", SqlDbType.NVarChar, 50) { Direction = ParameterDirection.Output }
        };

                await _context.Database.ExecuteSqlRawAsync(sql, parameters);

                // Retrieve the output values
                var returnCode = (int)parameters[^2].Value; // Second last parameter is ReturnCode
                var returnStatus = (string)parameters[^1].Value; // Last parameter is ReturnStatus

                return (returnCode, returnStatus);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while adding the transaction.", ex);
            }
        }




        //    public async Task<List<TransactionViewModel>> GetTransactionsAsync(
        //        string senderName,
        //        string receiverName,
        //        string senderAddress,
        //        string receiverAddress,
        //        string bankName,
        //        string accountNumber,
        //        decimal transferAmount,
        //        decimal payoutAmount,
        //        string currency,
        //        string senderCountry,
        //        string receiverCountry,
        //        string status,
        //        string senderIdentity,
        //        string receiverIdentity,
        //        string senderMobile,
        //        string receiverMobile,
        //        decimal exchangeRate,
        //        decimal serviceCharge)
        //    {
        //        var sql = @"
        //    EXEC Proc_Transaction 
        //            @Flag = @Flag, 
        //            @SenderName = @SenderName, 
        //            @ReceiverName = @ReceiverName, 
        //            @SenderAddress = @SenderAddress, 
        //            @ReceiverAddress = @ReceiverAddress, 
        //            @BankName = @BankName, 
        //            @AccountNumber = @AccountNumber, 
        //            @TransferAmount = @TransferAmount, 
        //            @PayoutAmount = @PayoutAmount, 
        //            @Currency = @Currency, 
        //            @SenderCountry = @SenderCountry, 
        //            @ReceiverCountry = @ReceiverCountry, 
        //            @Status = @Status, 
        //            @SIdentity = @SIdentity, 
        //            @RIdentity = @RIdentity, 
        //            @SMobile = @SMobile, 
        //            @RMobile = @RMobile, 
        //            @ExchangeRate = @ExchangeRate, 
        //            @ServiceCharge = @ServiceCharge, 
        //            @ReturnCode = @ReturnCode OUTPUT, 
        //            @ReturnStatus = @ReturnStatus OUTPUT";

        //        var parameters = new[]
        //        {
        //    new SqlParameter("@Flag", "s"),
        //        new SqlParameter("@SenderName", senderName ?? (object)DBNull.Value),
        //        new SqlParameter("@ReceiverName", receiverName ?? (object)DBNull.Value),
        //        new SqlParameter("@SenderAddress", senderAddress ?? (object)DBNull.Value),
        //        new SqlParameter("@ReceiverAddress", receiverAddress ?? (object)DBNull.Value),
        //        new SqlParameter("@BankName", bankName ?? (object)DBNull.Value),
        //        new SqlParameter("@AccountNumber", accountNumber ?? (object)DBNull.Value),
        //        new SqlParameter("@TransferAmount", transferAmount),
        //        new SqlParameter("@PayoutAmount", payoutAmount),
        //        new SqlParameter("@Currency", currency ?? (object)DBNull.Value),
        //        new SqlParameter("@SenderCountry", senderCountry ?? (object)DBNull.Value),
        //        new SqlParameter("@ReceiverCountry", receiverCountry ?? (object)DBNull.Value),
        //        new SqlParameter("@Status", status ?? (object)DBNull.Value),
        //        new SqlParameter("@SIdentity", senderIdentity ?? (object)DBNull.Value),
        //        new SqlParameter("@RIdentity", receiverIdentity ?? (object)DBNull.Value),
        //        new SqlParameter("@SMobile", senderMobile ?? (object)DBNull.Value),
        //        new SqlParameter("@RMobile", receiverMobile ?? (object)DBNull.Value),
        //        new SqlParameter("@ExchangeRate", exchangeRate),
        //        new SqlParameter("@ServiceCharge", serviceCharge),
        //        new SqlParameter("@ReturnCode", SqlDbType.Int) { Direction = ParameterDirection.Output },
        //        new SqlParameter("@ReturnStatus", SqlDbType.NVarChar, 50) { Direction = ParameterDirection.Output }
        //};

        //        // Load data directly into memory with ToListAsync()
        //        var transactionEntities = await _context.Transactions
        //            .FromSqlRaw(sql, parameters)
        //            .ToListAsync(); // No need for AsEnumerable()

        //        // Map the data to the view model
        //        return transactionEntities.Select(t => new TransactionViewModel
        //        {
        //            Id = t.Id,
        //            TransferAmount = t.TransferAmount ?? 0,
        //            SenderName = t.SenderName,
        //            SenderAddress = t.SenderAddress,
        //            SenderCountry = t.SenderCountry,
        //            SMobile = t.Smobile,
        //            SCurrency = t.Scurrency,
        //            ReceiverName = t.ReceiverName,
        //            ReceiverAddress = t.ReceiverAddress,
        //            ReceiverCountry = t.ReceiverCountry,
        //            RMobile = t.Rmobile,
        //            RCurrency = t.Rcurrency,
        //            PayoutAmount = t.PayoutAmount ?? 0
        //        }).ToList();
        //    }


        public async Task<List<Transaction>> GetTransactionsAsync(
     string flag,
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
     decimal serviceCharge)
        {
            var pflag = new SqlParameter("@Flag", flag);
            var psenderName = new SqlParameter("@SenderName", senderName ?? (object)DBNull.Value);
            var preceiverName = new SqlParameter("@ReceiverName", receiverName ?? (object)DBNull.Value);
            var psenderAddress = new SqlParameter("@SenderAddress", senderAddress ?? (object)DBNull.Value);
            var preceiverAddress = new SqlParameter("@ReceiverAddress", receiverAddress ?? (object)DBNull.Value);
            var pbankName = new SqlParameter("@BankName", bankName ?? (object)DBNull.Value);
            var paccountNumber = new SqlParameter("@AccountNumber", accountNumber ?? (object)DBNull.Value);
            var ptransferAmount = new SqlParameter("@TransferAmount", transferAmount);
            var ppayoutAmount = new SqlParameter("@PayoutAmount", payoutAmount);
            var pcurrency = new SqlParameter("@Currency", currency ?? (object)DBNull.Value);
            var psenderCountry = new SqlParameter("@SenderCountry", senderCountry ?? (object)DBNull.Value);
            var preceiverCountry = new SqlParameter("@ReceiverCountry", receiverCountry ?? (object)DBNull.Value);
            var pstatus = new SqlParameter("@Status", status ?? (object)DBNull.Value);
            var psenderIdentity = new SqlParameter("@SIdentity", senderIdentity ?? (object)DBNull.Value);
            var preceiverIdentity = new SqlParameter("@RIdentity", receiverIdentity ?? (object)DBNull.Value);
            var psenderMobile = new SqlParameter("@SMobile", senderMobile ?? (object)DBNull.Value);
            var preceiverMobile = new SqlParameter("@RMobile", receiverMobile ?? (object)DBNull.Value);
            var pexchangeRate = new SqlParameter("@ExchangeRate", exchangeRate);
            var pserviceCharge = new SqlParameter("@ServiceCharge", serviceCharge);

            // Declare the output parameters
            var pReturnCode = new SqlParameter("@ReturnCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var pReturnStatus = new SqlParameter("@ReturnStatus", SqlDbType.NVarChar, 50) { Direction = ParameterDirection.Output };

            // Execute the stored procedure and retrieve the results
            var transactions = await _context.Transactions
                .FromSqlRaw("EXEC Proc_Transaction @Flag, @SenderName, @ReceiverName, @SenderAddress, @ReceiverAddress, @BankName, @AccountNumber, @TransferAmount, @PayoutAmount, @Currency, @SenderCountry, @ReceiverCountry, @Status, @SIdentity, @RIdentity, @SMobile, @RMobile, @ExchangeRate, @ServiceCharge, @ReturnCode OUTPUT, @ReturnStatus OUTPUT",
                    pflag, psenderName, preceiverName, psenderAddress, preceiverAddress, pbankName, paccountNumber, ptransferAmount, ppayoutAmount, pcurrency, psenderCountry, preceiverCountry, pstatus, psenderIdentity, preceiverIdentity, psenderMobile, preceiverMobile, pexchangeRate, pserviceCharge, pReturnCode, pReturnStatus)
                .ToListAsync();

            // Retrieve the output values
            int returnCode = 0;
            string returnStatus = "y";

            // Optionally, handle the output values as needed (e.g., log, throw exceptions, etc.)
            if (returnCode != 0)
            {
                // Handle error (you might want to throw an exception or return a failure result)
                throw new Exception($"Error: {returnStatus}");
            }

            return transactions;
        }

        //public async Task<(int ReturnCode, string ReturnStatus)> AddTransactionAsync(
        //    string senderName,
        //    string receiverName,
        //    string senderAddress,
        //    string receiverAddress,
        //    string bankName,
        //    string accountNumber,
        //    decimal transferAmount,
        //    decimal payoutAmount,
        //    string currency,
        //    string senderCountry,
        //    string receiverCountry,
        //    string status,
        //     string senderIdentity,   
        //    string receiverIdentity, 
        //    string senderMobile,     
        //    string receiverMobile,   
        //    decimal exchangeRate,    
        //    decimal serviceCharge    
        //)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    using (var command = new SqlCommand("Proc_Transaction", connection))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;

        //        // Input parameters
        //        command.Parameters.AddWithValue("@Flag", "i");
        //        command.Parameters.AddWithValue("@SenderName", senderName);
        //        command.Parameters.AddWithValue("@ReceiverName", receiverName);
        //        command.Parameters.AddWithValue("@SenderAddress", senderAddress);
        //        command.Parameters.AddWithValue("@ReceiverAddress", receiverAddress);
        //        command.Parameters.AddWithValue("@BankName", bankName);
        //        command.Parameters.AddWithValue("@AccountNumber", accountNumber);
        //        command.Parameters.AddWithValue("@TransferAmount", transferAmount);
        //        command.Parameters.AddWithValue("@PayoutAmount", payoutAmount);
        //        command.Parameters.AddWithValue("@Currency", currency);
        //        command.Parameters.AddWithValue("@SenderCountry", senderCountry);
        //        command.Parameters.AddWithValue("@ReceiverCountry", receiverCountry);
        //        command.Parameters.AddWithValue("@Status", status);
        //        command.Parameters.AddWithValue("@SIdentity", senderIdentity);
        //        command.Parameters.AddWithValue("@RIdentity", receiverIdentity);
        //        command.Parameters.AddWithValue("@SMobile", senderMobile);
        //        command.Parameters.AddWithValue("@RMobile", receiverMobile);
        //        command.Parameters.AddWithValue("@ExchangeRate", exchangeRate);
        //        command.Parameters.AddWithValue("@ServiceCharge", serviceCharge);

        //        // Output parameters
        //        var returnCodeParam = new SqlParameter("@ReturnCode", SqlDbType.Int)
        //        {
        //            Direction = ParameterDirection.Output
        //        };
        //        command.Parameters.Add(returnCodeParam);

        //        var returnStatusParam = new SqlParameter("@ReturnStatus", SqlDbType.NVarChar, 50)
        //        {
        //            Direction = ParameterDirection.Output
        //        };
        //        command.Parameters.Add(returnStatusParam);

        //        await connection.OpenAsync();
        //        await command.ExecuteNonQueryAsync();

        //        // Retrieve output values
        //        var returnCode = (int)returnCodeParam.Value;
        //        var returnStatus = (string)returnStatusParam.Value;

        //        return (returnCode, returnStatus);
        //    }
        //}

        //public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        //{
        //    var transactions = new List<Transaction>();

        //    using (var connection = new SqlConnection(_connectionString))
        //    using (var command = new SqlCommand("Proc_Transaction", connection))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("@Flag", "s");

        //        await connection.OpenAsync();
        //        using (var reader = await command.ExecuteReaderAsync())
        //        {
        //            while (await reader.ReadAsync())
        //            {
        //                transactions.Add(new Transaction
        //                {
        //                    Id = reader.GetInt32("Id"),
        //                    SenderName = reader.GetString("SenderName"),
        //                    ReceiverName = reader.GetString("ReceiverName"),
        //                    SenderAddress = reader.GetString("SenderAddress"),
        //                    ReceiverAddress = reader.GetString("ReceiverAddress"),
        //                    BankName = reader.GetString("BankName"),
        //                    AccountNumber = reader.GetString("AccountNumber"),
        //                    TransferAmount = reader.GetDecimal("TransferAmount"),
        //                    PayoutAmount = reader.GetDecimal("PayoutAmount"),
        //                    Currency = reader.GetString("Currency"),
        //                    SenderCountry = reader.GetString("SenderCountry"),
        //                    ReceiverCountry = reader.GetString("ReceiverCountry"),
        //                    Status = reader.GetString("Status")
        //                });
        //            }
        //        }
        //    }

        //    return transactions;
        //}
    }

}

