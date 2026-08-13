using System.ComponentModel.DataAnnotations;

namespace AlocacaoVeiculosAssistencia.Application.DTOs.PlanosAssistencia
{
    public class PlanoUpdateDtos
    {
        [Required(ErrorMessage = "O Plano  é obrigatório.")]
        public string Plano { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O valor da cobertura é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor da cobertura deve ser um número positivo.")]
        public decimal ValorCobertura { get; set; }

        [Required(ErrorMessage = "A empresa é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A empresa é inválida.")]
        public int EmpresaId { get; set; }
    }
}
