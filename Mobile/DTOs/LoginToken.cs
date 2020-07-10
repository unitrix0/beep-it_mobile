namespace Mobile.DTOs
{
    public class LoginToken
    {
        public string IdentityToken { get; set; }
        public string PermissionsToken { get; set; }
        public User MappedUser { get; set; }
    }
}