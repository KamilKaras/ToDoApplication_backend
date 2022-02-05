using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoAplication.Configuration;
using ToDoAplication.Models;

namespace ToDoAplication.Controllers
{

    [ApiController]
    [Route("[controller]")]
      
    public class AuthController: ControllerBase
    {
        public readonly UserManager<AplicationUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        public AuthController(UserManager<AplicationUser> userManager, IOptionsMonitor<JwtConfig> jwtConfig)
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig.CurrentValue;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto userRegistrationRequestDto)
        {   
            if(!ModelState.IsValid)
            {
                var response = new RegistrationResponse {
                    Sucssess = false,
                    Errors = new List<string> { 
                        "Invalid Payload"
                    }                    
                };
                return BadRequest(response);
            }

            var existingUser = await _userManager.FindByEmailAsync(userRegistrationRequestDto.Email);

            if(existingUser is not null)
            {
                var response = new RegistrationResponse
                {
                    Sucssess = false,
                    Errors = new List<string> {
                        "User with this email already exists"
                    }
                };
                return BadRequest(response);
            }

            var newUser = new AplicationUser
            {
                Email = userRegistrationRequestDto.Email,
                UserName = userRegistrationRequestDto.Login,
            };

            var isCreated = await _userManager.CreateAsync(newUser, userRegistrationRequestDto.Password);

            if(isCreated.Succeeded)
            {
                var jwtToken = GenerateJwtToken(newUser);
                var response = new RegistrationResponse
                {
                    Sucssess = true,
                    Token = jwtToken
         
                };
                return Ok(response);
            }

            var finalResponse = new RegistrationResponse
            {
                Sucssess = false,
                Errors = new List<string> {
                        "Invalid Payload"
                    }
            };
            return BadRequest(finalResponse);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto userLoginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                var response = new RegistrationResponse
                {
                    Sucssess = false,
                    Errors = new List<string> {
                        "Invalid Payload"
                    }
                };
                return BadRequest(response);
            }

            var existingUser = await _userManager.FindByEmailAsync(userLoginRequestDto.Email);

            if (existingUser is null)
            {
                var response = new RegistrationResponse
                {
                    Sucssess = false,
                    Errors = new List<string> {
                        "Invalid login request"
                    }
                };
                return BadRequest(response);
            }

            var passwordCorrect = await _userManager.CheckPasswordAsync(existingUser, userLoginRequestDto.Password);

            if(!passwordCorrect)
            {
                var response = new RegistrationResponse
                {
                    Sucssess = false,
                    Errors = new List<string> {
                        "Invalid password"
                    }
                };
                return BadRequest(response);
            }

            var jwtToken = GenerateJwtToken(existingUser);

            return Ok(new LoginResponse {
                Sucssess = true,
                Token = jwtToken,
            });
        }

        private string GenerateJwtToken(AplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }
                ),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
