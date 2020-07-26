namespace Mobile.DTOs
{
    public class LoginResponse
    {
        public string IdentityToken { get; set; }
        public string RefreshToken { get; set; }
        public string PermissionsToken { get; set; }
        public User MappedUser { get; set; }
    }
}