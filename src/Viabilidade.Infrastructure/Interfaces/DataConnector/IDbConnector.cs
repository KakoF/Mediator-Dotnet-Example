using System.Data;

namespace Viabilidade.Infrastructure.Interfaces.DataConnector
{
    public interface IDbConnector : IDisposable
    {
        IDbConnection dbConnection { get; }
        IDbTransaction dbTransaction { get; set; }
        IDbTransaction BeginTransaction(IsolationLevel isolation);
    }
}
