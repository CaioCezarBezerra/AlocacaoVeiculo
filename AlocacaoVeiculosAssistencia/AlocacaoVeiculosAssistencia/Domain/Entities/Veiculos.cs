namespace AlocacaoVeiculosAssistencia.Domain.Entities
{
    public class Veiculos
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public int GrupoId { get; set; }
        public GruposVeiculos Grupo { get; set; }

        public ICollection<VeiculosAssistencia> Assistencias { get; set; }
    = new List<VeiculosAssistencia>();

    }
}
