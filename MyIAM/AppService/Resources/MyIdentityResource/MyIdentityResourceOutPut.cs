namespace MyIAM.AppService.Resources.MyIdentityResource
{
    public class MyIdentityResourceOutPut
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ICollection<string> UserClaims { get; set; } = new HashSet<string>();
    }
}
