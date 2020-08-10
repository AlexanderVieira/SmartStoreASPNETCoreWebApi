using System.Collections.Generic;
using System.Linq;

namespace SmartStore.Domain.Entities
{
    public abstract class Validacao
    {
        private List<string> _mensagensValidacao { get; set; }

        private List<string> MensagemValidacao {
            get { return _mensagensValidacao ?? (_mensagensValidacao = new List<string>()); }
        }

        public bool EhValido {
            get { return !MensagemValidacao.Any(); }
        }

        public abstract void Validate();

        protected void LimparMensagensValidacao()
        {
            MensagemValidacao.Clear();
        }

        protected void AdicionarCritica(string mensagem)
        {
            MensagemValidacao.Add(mensagem);
        }

        public string ObterMensagensValidacao()
        {
            return string.Join(". ", MensagemValidacao);
        }  
               
    }
}
