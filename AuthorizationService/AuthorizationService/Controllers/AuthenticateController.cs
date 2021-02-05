using AuthorizationService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationService.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using AuthorizationService.Service;

namespace AuthorizationService.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private IAuthenticationService authenticationService;
        private IConfiguration configuration;

        public AuthenticateController(IAuthenticationService authenticationService, IConfiguration configuration)
        {
            this.authenticationService = authenticationService;
            this.configuration = configuration;
        }


        //Action Method to Generate JWT 
        [HttpGet]
        public IActionResult TokenGeneration (string name,string password )
        {   
            User user =new User { Name = name, Password = password };
            
            //check if user exist using the method Authentication() in JwtauthenticationService
            bool isUserExist = authenticationService.Authentication(user.Name,user.Password);


            if (!isUserExist) return new UnauthorizedResult();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwtoken:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var claims = new List<Claim>
            {
                new Claim("UserId", user.UserId.ToString())
            };


            var token = new JwtSecurityToken(
                        issuer: configuration["Jwtoken:Issuer"],
                        audience: configuration["Jwtoken:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: credentials);

            return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

       
    }
}
