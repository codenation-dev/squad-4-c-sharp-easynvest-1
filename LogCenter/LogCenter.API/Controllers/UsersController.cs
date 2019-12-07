using Microsoft.AspNetCore.Mvc;
using LogCenter.Infra.Database;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LogCenter.API.Models;

namespace LogCenter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext db;

        public UsersController(DatabaseContext db)
        {
             this.db = db;
        }

        [HttpPost, Route("[action]")]
        public IActionResult Authenticate(User reqUser)
        {
            var user = db.Users.SingleOrDefault(x => x.Email == reqUser.Email && x.Password == reqUser.Password);

            // return null if user not found
            if (user == null)
                return new NotFoundResult();

            // authentication successful so generate jwt token
            var key = Encoding.ASCII.GetBytes("_n0d0nuts4U_SECRET_");
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.SerialNumber, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("LogCenterToken", user.Token)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            if (user != null)
                return Ok(tokenHandler.WriteToken(token));

            return BadRequest(new { message = "Username or password is incorrect" });
        }

        [HttpPost, Route("[action]")]
        public IActionResult Register(User reqUser)
        {
            var user = db.Users.SingleOrDefault(x => x.Email == reqUser.Email);

            // return null if user not found
            if (user != null)
                return new ConflictResult();

            var newUser = new Domain.Entities.User() {
                Email = reqUser.Email,
                Nome = reqUser.Nome,
                CreationDate = DateTime.Now,
                Password = reqUser.Password,
                Token = Guid.NewGuid().ToString()
            };

            db.Users.Add(newUser);
            db.SaveChanges();

            // erase password
            newUser.Password = null;

            return Ok(newUser);
        }
    }
}