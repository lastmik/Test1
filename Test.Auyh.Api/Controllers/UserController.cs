using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Auyth.Api.Data;
using Test.Auyth.Api.Models;

namespace Test.Auyth.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        public UserController(IUsersRepository users)
        {
            _usersRepository = users;
        }

        [HttpGet("user")]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            return Ok(_usersRepository.Get()); 
        }
    }
}
