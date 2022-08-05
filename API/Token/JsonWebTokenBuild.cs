using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace API.Token
{
    public class JsonWebTokenBuild
    {
        private SecurityKey securityKey = null;
        private string subject = "";
        private string issuer = "";
        private string audience = "";
        private Dictionary<string, string> claims = new Dictionary<string, string>();
        private int expiryInMinutes = 5;


        public JsonWebTokenBuild AddSecurityKey(SecurityKey securityKey)
        {
            this.securityKey = securityKey;
            return this;
        }

        public JsonWebTokenBuild AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        public JsonWebTokenBuild AddIssuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }

        public JsonWebTokenBuild AddAudience(string audience)
        {
            this.audience = audience;
            return this;
        }

        public JsonWebTokenBuild AddClaim(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }

        public JsonWebTokenBuild AddClaims(Dictionary<string, string> claims)
        {
            this.claims.Union(claims);
            return this;
        }

        public JsonWebTokenBuild AddExpiry(int expiryInMinutes)
        {
            this.expiryInMinutes = expiryInMinutes;
            return this;
        }


        private void EnsureArguments()
        {
            if (this.securityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(this.subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrEmpty(this.issuer))
                throw new ArgumentNullException("Issuer");

            if (string.IsNullOrEmpty(this.audience))
                throw new ArgumentNullException("Audience");
        }

        public JsonWebToken Builder()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,this.subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                issuer: this.issuer,
                audience: this.audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: new SigningCredentials(
                                                   this.securityKey,
                                                   SecurityAlgorithms.HmacSha256)

                );

            return new JsonWebToken(token);

        }
    }
}
