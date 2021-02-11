using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Infrastructure
{
    public class TokenModel
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "test-key-11022021";
        public const int LIFETIME = 320;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
