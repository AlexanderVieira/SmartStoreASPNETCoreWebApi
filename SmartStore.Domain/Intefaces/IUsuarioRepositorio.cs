using SmartStore.Domain.Entities;

namespace SmartStore.Domain.Intefaces
{
    public interface IUsuarioRepositorio : IBaseRepositorio<Usuario>
    {
        Usuario Obter(string email, string senha);
        Usuario Obter(string email);
        Usuario ObterPorTagId(string tag);
    }
}
