using Mobile.Helpers;

namespace Mobile.Data
{
    public class PermissionsToken
    {
        public int UserId { get; }
        public PermissionFlags Flags { get; }
        public string PermissionsSerial { get; }
        public int EnvironmentId { get; }
        public bool IsValid { get; }

        public PermissionsToken(string tokenString)
        {
        }
    }
}