using AsistencIA_DOMAIN.Data;

namespace AsistencIA_DOMAIN.Core.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<Usuarios> SignIn(string email, string pwd);
        Task<bool> ChangePwd(string usuario, string oldPassword, string newPassword);
    }
}
