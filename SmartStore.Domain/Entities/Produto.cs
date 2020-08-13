namespace SmartStore.Domain.Entities
{
    public class Produto : Validacao
    {
        public int Id { get; set; }
        public string TagRFID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string NomeArquivo { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Nome))
                AdicionarCritica("Nome do produto não foi informado");

            if (string.IsNullOrEmpty(Descricao))
                AdicionarCritica("Descrição não foi informada");

            if (string.IsNullOrEmpty(TagRFID))
            {
                AdicionarCritica("TagRFID não foi informada ");
            }

            if (TagRFID.Contains(" ") || (TagRFID == TagRFID.ToLower()))
            {
                TagRFID = TagRFID.ToUpper().Replace(" ", "");
            }
        }
    }
}
