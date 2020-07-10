namespace Mobile.Data
{
    public class IdentityToken
    {
        public string UserName { get; private set; }
        public int UserId { get; private set; }
        public string Roles { get; private set; }
        public bool IsValid { get; private set; }
        public string TokenString { get; private set; }

        public IdentityToken(string tokenString)
        {
            TokenString = tokenString;
        }
    }
}