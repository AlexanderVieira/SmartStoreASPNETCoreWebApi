using SmartStore.Domain.Entities;
using SmartStore.Domain.Intefaces;
using SmartStore.Infra.Context;
using System.Linq;

namespace SmartStore.Infra.Repositories
{
    public class UsuarioRepositorio : BaseRepositorio<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(SmartStoreDbContext ctx) : base(ctx)
        {           
        }

        public Usuario Obter(string email, string senha)
        {
            return _ctx.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }

        public Usuario Obter(string email)
        {
            return _ctx.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public Usuario ObterPorTagId(string tag)
        {
            return _ctx.Usuarios.FirstOrDefault(u => u.TagClient == tag);
        }
    }
}
