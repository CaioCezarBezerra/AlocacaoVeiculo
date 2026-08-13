namespace AlocacaoVeiculosAssistencia.Domain.Entities
{
    public class GruposVeiculos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public ICollection<Veiculos> Veiculos { get; set; } = new List<Veiculos>();


    }
}
