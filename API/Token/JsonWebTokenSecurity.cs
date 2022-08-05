using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API.Token
{
    public class JsonWebTokenSecurity
    {
        // Gerador chave secreta
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
