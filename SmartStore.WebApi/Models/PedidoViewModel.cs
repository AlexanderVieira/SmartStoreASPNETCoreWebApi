using SmartStore.Domain.Entities;

namespace SmartStore.WebApi.Models
{
    public class PedidoViewModel
    {
        public Usuario Usuario { get; set; }
        public Produto[] Produtos { get; set; }
        public string CarrinhoId { get; set; }
    }
}
