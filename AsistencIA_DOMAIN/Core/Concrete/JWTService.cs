using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AsistencIA_DOMAIN.Core.Entities;
using AsistencIA_DOMAIN.Core.Interfaces;
using AsistencIA_DOMAIN.Core.Concrete;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AsistencIA_DOMAIN.Data;

namespace AsistencIA_DOMAIN.Core.Concrete
{
    public class JWTService : IJWTService
    {

        public JWTSettings _settings { get; }

        public JWTService(IOptions<JWTSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateJWToken(Usuarios usuarios)
        {
            var ssk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var sc = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(sc);

            var claims = new[] {
                new Claim(ClaimTypes.Name , usuarios.Apellidos),
                new Claim(ClaimTypes.Email , usuarios.Email),
                new Claim(ClaimTypes.DateOfBirth , usuarios.FechaNacimiento.ToString()),
                new Claim("UserId" , usuarios.IdUsuario.ToString()),
            };

            var payload = new JwtPayload(
                            _settings.Issuer,
                            _settings.Audience,
                            claims,
                            DateTime.UtcNow,
                            DateTime.UtcNow.AddMinutes(_settings.DurationInMinutes));
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}



