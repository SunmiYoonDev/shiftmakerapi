using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dayoff.BLL.Repositories;
using Dayoff.DAL.Models;
using Dayoff.DAL.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Dayoff.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }
        [HttpPost("login")]
        public IActionResult Login(UserForLoginResource userForLoginDto)
        {
            var loginUser = _repo.FindUserByEmail(userForLoginDto.email);
            if (loginUser == null)
                return Ok(new { error = "100" });
            if (loginUser.isDeleted)
                return Ok(new { error = "105" });
            if (!_repo.IsActiveUser(userForLoginDto.email))
                return Ok(new { error = "102" });

            var userFromRepo = _repo.Login(userForLoginDto.email, userForLoginDto.password);

            if (userFromRepo == null)
                return Ok(new { error = "101" });

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.firstName),
                new Claim(ClaimTypes.Role, userFromRepo.isAdmin.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //Expires = DateTime.Now.AddDays(180),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var data = new
            {
                userFromRepo.id,
                userFromRepo.firstName,
                userFromRepo.lastName,
                token = tokenHandler.WriteToken(token)
            };

            return Ok(new { data });
        }
        [HttpPost("register")]
        public IActionResult Register(UserForRegisterResource userForRegisterDto)
        {
            userForRegisterDto.email = userForRegisterDto.email.ToLower();

            var RegisterUser = _repo.FindUserByEmail(userForRegisterDto.email);
            if (RegisterUser != null)
            {

                if (RegisterUser.isDeleted)
                    return Ok(new { error = "105" });

                return Ok(new { error = "103" });
            }

            var userId = Guid.NewGuid();
            var companyId = Guid.NewGuid();

            var userToCreate = new User
            {
                id = userId,
                email = userForRegisterDto.email,
                firstName = userForRegisterDto.firstName,
                lastName = userForRegisterDto.lastName,
                isAdmin = userForRegisterDto.isAdmin,
                isActive = userForRegisterDto.isAdmin
            };

            var companyToCreate = new Company
            {
                id = companyId,
                name = userForRegisterDto.companyName,
                createdBy = userId
            };

            _repo.Register(userToCreate, companyToCreate, userForRegisterDto.password);

            var data = "success";

            return Ok(new { data });

        }

        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword(UserForForgotPasswordResource userForForgotPasswordDto)
        {
            var user = _repo.FindUserByEmail(userForForgotPasswordDto.email);

            if (user == null)
                return Ok(new { error = "100" });
            else if (!user.isActive)
                return Ok(new { error = "102" });
            else if (user.isDeleted)
                return Ok(new { error = "105" });


            _repo.ForgotPassword(userForForgotPasswordDto.email);

            var data = "success";

            return Ok(new { data });
        }

    }
}
