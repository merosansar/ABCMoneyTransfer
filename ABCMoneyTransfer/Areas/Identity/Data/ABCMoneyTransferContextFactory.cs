using ABCMoneyTransfer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ABCMoneyTransfer.Areas.Identity.Data
{
    public class ABCMoneyTransferContextFactory : IDesignTimeDbContextFactory<ABCMoneyTransferContext>
    {
        public ABCMoneyTransferContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ABCMoneyTransferContext>();

            // Build the configuration for reading from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // Ensure this is the right base directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Retrieve the connection string from appsettings.json
            var connectionString = configuration.GetConnectionString("ABCMoneyTransferContextConnection");

            // Use the connection string to configure DbContext
            optionsBuilder.UseSqlServer(connectionString);

            return new ABCMoneyTransferContext(optionsBuilder.Options);
        }
    
}
}
