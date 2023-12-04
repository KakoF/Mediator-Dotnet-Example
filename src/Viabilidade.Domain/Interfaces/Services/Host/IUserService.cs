namespace Viabilidade.Domain.Interfaces.Services.Host
{
    public interface IUserService
    {
        Task<string> GetUserNameAsync(Guid id);
    }
}
