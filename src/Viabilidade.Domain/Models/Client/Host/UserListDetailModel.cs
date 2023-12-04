namespace Viabilidade.Domain.Models.Client.Host
{
    public class UserListDetailModel
    {
        public int Qtd { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<UserDetailModel> Users { get; set; }
    }
   
}