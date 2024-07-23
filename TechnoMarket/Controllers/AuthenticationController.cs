using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechnoMarket.Application.IServices;
using TechnoMarket.Application.Services;
using TechnoMarket.Domain.DTOs;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] CredentialsRequest credentials)
        {
            //PASO 1: Validar las credenciales
            UserModel? userLogged = _userService.CheckCredentials(credentials);
            if (userLogged is not null)
            {
                //Paso 2: Crear el token
                var saltEncrypted = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"])); //Traemos la SecretKey del Json. agregar antes: using Microsoft.IdentityModel.Tokens;

                var signature = new SigningCredentials(saltEncrypted, SecurityAlgorithms.HmacSha256);

                //Los claims son datos en clave->valor que nos permite guardar data del usuario.
                var claimsForToken = new List<Claim>
                {
                    new Claim("sub", userLogged.Id.ToString()), // "sub" es una key estándar que significa unique user identifier, es decir, si mandamos el id del usuario por convención lo hacemos con la key "sub".
                    new Claim("role", userLogged.Role.ToString()) // Debería venir del usuario
                };

                var jwtSecurityToken = new JwtSecurityToken( // agregar using System.IdentityModel.Tokens.Jwt; Acá es donde se crea el token con toda la data que le pasamos antes.
                  _configuration["Authentication:Issuer"],
                  _configuration["Authentication:Audience"],
                  claimsForToken, // información que mandamos
                  DateTime.UtcNow, // fecha de creación
                  DateTime.UtcNow.AddHours(1), // fecha de expiración
                  signature // usado para la generación de la asignature
                );

                var tokenToReturn = new JwtSecurityTokenHandler() // Pasamos el token a string
                    .WriteToken(jwtSecurityToken);

                return Ok(tokenToReturn);
            }

            return Unauthorized();
        }
    }
}