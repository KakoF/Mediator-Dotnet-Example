namespace Viabilidade.Domain.Models.Client.Host
{
    public class UserInfoModel
    {
        public Guid Sub { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
