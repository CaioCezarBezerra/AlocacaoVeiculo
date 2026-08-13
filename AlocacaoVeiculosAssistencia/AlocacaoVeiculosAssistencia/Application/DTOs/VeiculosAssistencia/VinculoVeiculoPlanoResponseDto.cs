namespace AlocacaoVeiculosAssistencia.Application.DTOs.VeiculosAssistencia
{
    public class VinculoVeiculoPlanoResponseDto
    {
        public int Id { get; set; }
        public int VeiculoId { get; set; }
        public string Veiculo { get; set; } = string.Empty;
        public int PlanoId { get; set; }
        public string Plano { get; set; } = string.Empty;
    }
}
