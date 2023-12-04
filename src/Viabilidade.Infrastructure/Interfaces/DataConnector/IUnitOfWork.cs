using System.Data;

namespace Viabilidade.Infrastructure.Interfaces.DataConnector
{
    public interface IUnitOfWork
    {
        IDbConnector dbConnector { get; }

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void Dispose();
    }
}
