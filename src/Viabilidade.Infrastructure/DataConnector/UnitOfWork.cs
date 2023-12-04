using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.DataConnector
{
    public class UnitOfWork : IUnitOfWork
    {

        public IDbConnector dbConnector { get; }

        public UnitOfWork(IDbConnector dbConnector)
        {
            this.dbConnector = dbConnector;
        }

        public void BeginTransaction()
        {
            dbConnector.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
        }

        public void CommitTransaction()
        {
            if (dbConnector.dbConnection.State == System.Data.ConnectionState.Open)
            {
                dbConnector.dbTransaction.Commit();
            }
        }

        public void RollbackTransaction()
        {
            if (dbConnector.dbConnection.State == System.Data.ConnectionState.Open)
            {
                if (dbConnector.dbTransaction != null)
                    dbConnector.dbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            dbConnector.Dispose();
        }
    }
}