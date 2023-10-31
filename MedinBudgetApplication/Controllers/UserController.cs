using API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Services;
using Service.Services.ServiceInterfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _services;
        private readonly IConfiguration _config;
        public UserController(IUserService service, IConfiguration config)
        {
            _services = service;
            _config = config;
        }
      
        

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            Console.WriteLine("hej");
            try
            {

                if (loginDTO.UserInput != null)
                {
                    var usernameChecked = _services.Login(loginDTO.UserInput, loginDTO.Password);

                    if (usernameChecked == loginDTO.UserInput)
                    {
                        var LoginCredentials = new TokenDTO();
                        GetCredentials(loginDTO, LoginCredentials);
                        var token = GenerateToken(LoginCredentials);

                        return Ok(token);
                    }
                    else
                    {
                        return BadRequest(usernameChecked);
                    }
                }
                else
                {
                    return BadRequest("Incorrect input"); // throw? // userInput == 0?
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void GetCredentials(LoginDTO loginDTO, TokenDTO LoginCredentials)
        {
            LoginCredentials.UserInput = loginDTO.UserInput;
            LoginCredentials.userId = _services.GetUserId(loginDTO.UserInput);
            LoginCredentials.Password = loginDTO.Password;
        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public IActionResult RegisterUser([FromBody] RegistrationDTO registrationDTO)
        {
            string registrationMessage = _services.RegisterNewUser(registrationDTO.Email,
               registrationDTO.Password,
               registrationDTO.UserName,
               registrationDTO.FirstName,
               registrationDTO.LastName);

            if (registrationMessage == "Users created, returning to login page.")
            {
                return Ok(registrationMessage);
            }
            else
            {
                return BadRequest(registrationMessage);
            }
        }

        private string GenerateToken(TokenDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub,user.userId.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
