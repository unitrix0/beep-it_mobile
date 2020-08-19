using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Mobile.Helpers;

namespace Mobile.Data
{
    public class PermissionsToken
    {
        private readonly int _userId;

        public int UserId => _userId;
        public PermissionFlags Flags { get; }
        public string PermissionsSerial { get; }
        public int EnvironmentId { get; }
        public bool IsValid { get; }

        public PermissionsToken(string tokenString)
        {
            if (string.IsNullOrEmpty(tokenString))
            {
                Flags = 0;
                PermissionsSerial = "";
                IsValid = false;
            }
            else
            {
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken token = handler.ReadJwtToken(tokenString);
                IsValid = token != null;

                int.TryParse(token?.Claims.Single(c => c.Type == JwtRegisteredClaimNames.NameId).Value, out _userId);

                string permissionBits = token?.Claims.Single(c => c.Type == "permissions").Value;
                Flags = (PermissionFlags)Convert.ToInt32(permissionBits, 2);
                PermissionsSerial = token?.Claims.Single(c => c.Type == "permissions_serial").Value ?? "0";
                EnvironmentId = Convert.ToInt32(token?.Claims.Single(c => c.Type == "environment_id").Value);
            }
        }
    }
}