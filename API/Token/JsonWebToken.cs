using System.IdentityModel.Tokens.Jwt;

namespace API.Token
{
    public class JsonWebToken
    {
        private JwtSecurityToken token;

        internal JsonWebToken(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime ValidTo => token.ValidTo;
        public string value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}
