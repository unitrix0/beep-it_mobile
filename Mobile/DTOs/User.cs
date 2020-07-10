namespace Mobile.DTOs
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}