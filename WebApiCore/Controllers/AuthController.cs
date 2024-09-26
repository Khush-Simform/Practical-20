using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiCore.Models;
using WebApiCore.Services.CustomersServices;
using WebApiCore.Services.UnitOfWork;
using static WebApiCore.Services.CustomersServices.UserServices;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userService;
        private readonly IConfiguration _configuration;
        public AuthController(IUserServices userServices, IConfiguration configuration)
        {
            _userService = userServices;
            this._configuration = configuration;
        }

               

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            User user= new User();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.UserName= request.UserName;
            user.PasswordHash = passwordHash;
            var result = _userService.AddUser(user);

            return Ok(result);
 
        }


        [HttpPost("login")]
        public ActionResult<User>Login(UserDto request)
        {
            var result = _userService.GetSingleUser(request.UserName);
            if (result == null)
            {
                return BadRequest("User not found!!");
            }
            else
            {
                if (!BCrypt.Net.BCrypt.Verify(request.Password, result.PasswordHash))
                {
                    return BadRequest("Wrong Password!!");
                }
                else
                {
                    string token = CreateToken(result);
                    result.Token = token;
                    _userService.UpdateUser(result);
                    return Ok(token);
                }
                    

            }

          
            //if (user.UserName != request.UserName)
            //{
            //    return BadRequest("User not found!!");
            //}
            //if (!BCrypt.Net.BCrypt.Verify(request.Password,user.PasswordHash))
            //{
            //    return BadRequest("Wrong Password!!");
            //}
          
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials : creds

                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
