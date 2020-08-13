using SmartStore.Domain.Entities;

namespace SmartStore.Domain.Intefaces
{
    public interface IProdutoRepositorio : IBaseRepositorio<Produto>
    {
        Produto ObterPorTagId(string TagId);
    }
}
