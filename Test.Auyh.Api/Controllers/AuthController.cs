using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Test.Auyth.Api.Data;
using Test.Auyth.Api.Models;

namespace Test.Auyth.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IJwtService _jwtService;

        public AuthController(IUsersRepository usersRepository, IJwtService jwtService)
        {
            _usersRepository = usersRepository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(Register _user)
        {

            if (_user.Password != _user.PasswordConfirmation)
            {
                return Unauthorized();
            }
            User user = new User
            {
                Name = _user.Name,
                Login = _user.UserLogin,
                Password = BCrypt.Net.BCrypt.HashPassword(_user.Password)
            };
            _usersRepository.Create(user);

            return Created("succes", user);

        }
        [HttpPost("login")]
        public IActionResult Login(Login user)
        {
            User u = _usersRepository.GetByLogin(user.UserLogin);
            if (u == null || !BCrypt.Net.BCrypt.Verify(user.Password, u.Password))
                return BadRequest(new { message = "invalid user" });
            string jwt = _jwtService.GenerateJWT(u);
            Response.Cookies.Append("JwtToken", jwt, new CookieOptions { HttpOnly = true});
            return Ok(new { message = "success"});
        }
       


    }
}
