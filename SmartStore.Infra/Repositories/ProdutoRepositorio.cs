using SmartStore.Domain.Entities;
using SmartStore.Domain.Intefaces;
using SmartStore.Infra.Context;
using System.Linq;

namespace SmartStore.Infra.Repositories
{
    public class ProdutoRepositorio : BaseRepositorio<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(SmartStoreDbContext ctx) : base(ctx)
        {
        }

        public Produto ObterPorTagId(string TagId)
        {
            return _ctx.Produtos.FirstOrDefault(x => x.TagRFID.ToUpper() == TagId.ToUpper());
        }
    }
}
