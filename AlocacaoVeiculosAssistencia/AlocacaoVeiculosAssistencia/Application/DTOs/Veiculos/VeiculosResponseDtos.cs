namespace AlocacaoVeiculosAssistencia.Application.DTOs.Veiculos
{
    public class VeiculosResponseDtos
    {

        public int Id { get; set; }
        public string Modelo { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;
        public int GrupoId { get; set; }
        public string GrupoNome { get; set; } = string.Empty;
    }
}

