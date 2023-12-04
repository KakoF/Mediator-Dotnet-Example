namespace Viabilidade.Domain.Models.Client.Host
{
    public class UserDetailModel
    {
        public Guid UsersId { get; set; }
        public string UserName { get; set; }
        public string UserLogin { get; set; }
        public string UserEmail { get; set; }
        public string UserCellPhone { get; set; }
        public bool UserIsSuperUser { get; set; }
        public int UserLegacyId { get; set; }
        public DateTime UserCreatedOn { get; set; }
        public bool UserIsActive { get; set; }
        public IList<EntityUserDetailsViewModel> Entities { get; set; }
    }
    public class EntityUserDetailsViewModel
    {
        public Guid EntitiesId { get; set; }
        public string EntityName { get; set; }
        public int EntityLegacyId { get; set; }
        public List<ProductUserDetailsViewModel> Products { get; set; }
    }
    public class ProductUserDetailsViewModel
    {
        public Guid ProductsId { get; set; }
        public string ProductName { get; set; }
        public string ProductIcon { get; set; }
        public string ProductUrl { get; set; }
        public int ProductType { get; set; }
        public int ProductLegacyId { get; set; }
        public DateTime ProductCreatedOn { get; set; }
        public DateTime ProductUpdateddOn { get; set; }
        public bool ProductIsActive { get; set; }
        public List<RoleUserDetailsViewModel> Roles { get; set; }
    }
    public class RoleUserDetailsViewModel
    {
        public Guid RolesId { get; set; }
        public int RoleLegacyId { get; set; }
        public string RoleName { get; set; }
        public bool RoleIsActive { get; set; }
    }
}