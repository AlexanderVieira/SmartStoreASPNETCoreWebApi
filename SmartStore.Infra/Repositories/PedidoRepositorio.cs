using SmartStore.Domain.Entities;
using SmartStore.Domain.Intefaces;
using SmartStore.Infra.Context;

namespace SmartStore.Infra.Repositories
{
    public class PedidoRepositorio : BaseRepositorio<Pedido>, IPedidoRepositorio
    {
        public PedidoRepositorio(SmartStoreDbContext ctx) : base(ctx)
        {
        }
    }
}
