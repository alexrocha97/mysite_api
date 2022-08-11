using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API.Application.Token
{
    public class WebTokenSecurity
    {
         // Gerador chave secreta
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
