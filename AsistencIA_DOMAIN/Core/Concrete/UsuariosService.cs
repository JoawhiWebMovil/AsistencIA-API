using AsistencIA_DOMAIN.Core.Interfaces;
using AsistencIA_DOMAIN.Core.DTOs;

namespace AsistencIA_DOMAIN.Core.Concrete
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IJWTService _jwtService;

        public UsuariosService(IUsuariosRepository usuariosRepository, IJWTService jwtService)
        {
            _usuariosRepository = usuariosRepository;
            _jwtService = jwtService;
        }

        public async Task<UsuariosResponseAuthDTO> SignIn(string usuario, string password)
        {
            var user = await _usuariosRepository.SignIn(usuario, password);
            if (user == null) return null;

            var token = _jwtService.GenerateJWToken(user);

            var usuarioDTO = new UsuariosResponseAuthDTO()
            {
                IdUsuario = user.IdUsuario,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                Email = user.Email,
                Contrasena = user.Contrasena,
                Rol = user.Rol,
                FotoReferencia = user.FotoReferencia,
                FechaNacimiento = user.FechaNacimiento,
                Token = token
            };


            return usuarioDTO;
        }

        public async Task<bool> ChangePwd(string usuario, string oldPassword, string newPassword)
        {
            return await _usuariosRepository.ChangePwd(usuario, oldPassword, newPassword);
        }
    }
}


