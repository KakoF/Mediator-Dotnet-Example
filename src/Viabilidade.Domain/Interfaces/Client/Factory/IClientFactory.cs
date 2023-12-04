namespace Viabilidade.Domain.Interfaces.Client.Factory
{
    public interface IClientFactory<T, O>
    {
        Task<O> GetAsync(string path);

        Task<O> PostAsync(string path, T data);
    }
}
