using AsistencIA_DOMAIN.Core.Concrete;
using AsistencIA_DOMAIN.Data;

namespace AsistencIA_DOMAIN.Core.Interfaces
{
    public interface IJWTService
    {
        JWTSettings _settings { get; }

        string GenerateJWToken(Usuarios usuarios);
    }
}