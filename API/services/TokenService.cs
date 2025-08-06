using API.Interfaces;
using API.models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.services
{
    public class TokenService : ItokenService //implementation of the token service interface
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config ;
        }
       
        public string createToken(ApplicationUser user)
        {
            var secretKey = _config["AppSettings:TokenKey"];

            //cjeck if the secret key is null or empty
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("Token key is not configured in AppSettings.");
            }
            //create a new symmetric security key and signing credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName), //add the user id to the claims
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //add the user name to the claims
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) //add the user id to the claims
            };
            var token = new JwtSecurityToken
            (
                issuer: null, //issuer is not used in this case
                audience: null, //audience is not used in this case
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60), //token expiration time
                signingCredentials: creds //signing credentials
            );
            //return the generates tojken
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
