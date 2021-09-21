using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Test.Auyth.Api.Models;

namespace Test.Auyth.Api.Data
{
    public interface IJwtService
    {
        string GenerateJWT(User user);
    }
}
