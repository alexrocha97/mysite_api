using System.IdentityModel.Tokens.Jwt;

namespace API.Application.Token
{
    public class WebToken
    {
        private JwtSecurityToken token;

        internal WebToken(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime ValidTo => token.ValidTo;
        public string value => new JwtSecurityTokenHandler().WriteToken(this.token);
    }
}
