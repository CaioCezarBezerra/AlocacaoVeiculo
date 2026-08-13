using System.ComponentModel.DataAnnotations;

namespace AlocacaoVeiculosAssistencia.Application.DTOs.VeiculosAssistencia
{
    public class VinculoVeiculoPlanoCreateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "O veículo é obrigatório.")]
        public int VeiculoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O plano é obrigatório.")]
        public int PlanoId { get; set; }
    }
}