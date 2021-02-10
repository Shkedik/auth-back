using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Infrastructure
{
    public class TokenModel
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "itp_key123_kartashova";
        public const int LIFETIME = 320;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
