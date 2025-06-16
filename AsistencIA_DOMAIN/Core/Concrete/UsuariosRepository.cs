using Microsoft.EntityFrameworkCore;
using AsistencIA_DOMAIN.Core.Entities;
using AsistencIA_DOMAIN.Core.Interfaces;
using AsistencIA_DOMAIN.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AsistencIA_DOMAIN.Core.Concrete
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly DbAsistencIaDbContext _dbContext;

        public UsuariosRepository(DbAsistencIaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuarios> SignIn(string usuario, string pwd)
        {
            return await _dbContext
                    .Usuarios
                    .Where(u => u.IdUsuario == usuario && u.Contrasena == pwd && u.Estado)
                    .FirstOrDefaultAsync();
        }

        public async Task<bool> ChangePwd(string usuario, string oldPassword, string newPassword)
        {
            var personal = await _dbContext.Usuarios
                .FirstOrDefaultAsync(p => p.IdUsuario == usuario);

            if (personal == null || personal.Contrasena != oldPassword)
            {
                return false;
            }

            personal.Contrasena = newPassword;
            _dbContext.Usuarios.Update(personal);
            await _dbContext.SaveChangesAsync();
            return true;
        }


    }

}
