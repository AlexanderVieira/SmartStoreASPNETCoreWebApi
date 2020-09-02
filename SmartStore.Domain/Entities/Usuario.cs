using System.Collections.Generic;

namespace SmartStore.Domain.Entities
{
    public class Usuario : Validacao
    {
        public int Id { get; set; }
        public string TagClient { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public bool EhAdministrador { get; set; }

        /// <summary>
        /// Um Usuario Pode ter nenhum ou muitos pedidos
        /// </summary>
        public virtual ICollection<Pedido> Pedidos { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Email))
                AdicionarCritica("Email não informado.");

            if (string.IsNullOrEmpty(Senha))
                AdicionarCritica("Senha não informada.");
        }
    }
}
