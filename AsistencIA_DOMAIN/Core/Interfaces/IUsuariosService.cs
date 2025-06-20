using AsistencIA_DOMAIN.Core.DTOs;

namespace AsistencIA_DOMAIN.Core.Interfaces
{
    public interface IUsuariosService
    {
        Task<UsuariosResponseAuthDTO> SignIn(string email, string pwd);
        Task<bool> ChangePwd(string usuario, string oldPassword, string newPassword);
    }

}
