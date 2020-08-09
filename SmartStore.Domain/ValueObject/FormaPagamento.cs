using SmartStore.Domain.Enumerateds;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStore.Domain.ValueObject
{
    public class FormaPagamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public bool EhBoleto {
            get { return Id == (int)TipoFormaPagamentoEnum.Boleto; }
        }

        public bool EhCartaoCredito {
            get { return Id == (int)TipoFormaPagamentoEnum.CartaoCredito; }
        }

        public bool EhDeposito {
            get { return Id == (int)TipoFormaPagamentoEnum.Deposito; }
        }

        public bool Desconhecido {
            get { return Id == (int)TipoFormaPagamentoEnum.Desconhecido; }
        }

    }
}
