using Mobile.Helpers;

namespace Mobile.Data
{
    public class PermissionsToken
    {
        public int UserId { get; private set; }
        public PermissionFlags Flags { get; private set; }
        public string PermissionsSerial { get; private set; }
        public int EnvironmentId { get; private set; }
        public bool IsValid { get; private set; }

        public PermissionsToken(string tokenString)
        {
        }
    }
}