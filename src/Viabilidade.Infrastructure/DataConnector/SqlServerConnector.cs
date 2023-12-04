using System.Data;
using System.Data.SqlClient;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.DataConnector
{
    public class SqlServerConnector : IDbConnector
    {
        public SqlServerConnector(string connectionString)
        {
            dbConnection = SqlClientFactory.Instance.CreateConnection();
            dbConnection.ConnectionString = connectionString;
            dbConnection.Open();
        }

        public IDbConnection dbConnection { get; }

        public IDbTransaction dbTransaction { get; set; }

        public IDbTransaction BeginTransaction(IsolationLevel isolation)
        {
            if (dbTransaction != null)
            {
                return dbTransaction;
            }

            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            return (dbTransaction = dbConnection.BeginTransaction(isolation));
        }

        public void Dispose()
        {
            dbTransaction?.Dispose();
            dbConnection?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
