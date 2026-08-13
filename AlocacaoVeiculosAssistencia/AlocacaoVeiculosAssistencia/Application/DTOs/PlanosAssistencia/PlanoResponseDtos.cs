namespace AlocacaoVeiculosAssistencia.Application.DTOs.PlanosAssistencia
{
    public class PlanoResponseDtos
    {
        public int Id { get; set; }
        public string Plano { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal ValorCobertura { get; set; }
        public int EmpresaId { get; set; }
        public string EmpresaNome { get; set; } = string.Empty;
    }
}
