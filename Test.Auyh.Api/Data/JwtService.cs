using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test.Auyth.Api.Models;

namespace Test.Auyth.Api.Data
{
    public class JwtService: IJwtService
    {

        private readonly IOptions<AuthOptions> _options;
        public JwtService(IOptions<AuthOptions> options)
        {
            _options = options;
        }
        public string GenerateJWT(User user)
        {
            var par = _options.Value;
            var securityKey = par.GetSymmetricSecurityKey();
            var cred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claim = new List<Claim> { new Claim(JwtRegisteredClaimNames.NameId, user.Name) };

            JwtSecurityToken token = new JwtSecurityToken(par.Issuer,
                par.Audience,
                claim,
                expires: DateTime.Now.AddSeconds(par.TokenLifeTime),
                signingCredentials: cred);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
       
    }
}
