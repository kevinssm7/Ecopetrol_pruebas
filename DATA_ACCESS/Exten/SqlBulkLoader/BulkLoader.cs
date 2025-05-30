using DATA_ACCESS.Exten.SqlBulkLoader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_ACCESS.SqlBulkLoader
{
    class BulkLoader
    {
        public async Task BulkLoadTableAsync(ObjectDataReader reader, string tableName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["medicamentosConnectionString"].ConnectionString;
            using (var bulkCopy = new SqlBulkCopy(connectionString))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BulkCopyTimeout = 0;
                bulkCopy.BatchSize = 100000;
                await bulkCopy.WriteToServerAsync(reader);
            }

        }
    }
}
