using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStore.Domain.Entities
{
    public class Produto : Validacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string NomeArquivo { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Nome))
                AdicionarCritica("Nome do produto não foi informado");

            if (string.IsNullOrEmpty(Descricao))
                AdicionarCritica("Descrição não foi informado");
        }
    }
}
