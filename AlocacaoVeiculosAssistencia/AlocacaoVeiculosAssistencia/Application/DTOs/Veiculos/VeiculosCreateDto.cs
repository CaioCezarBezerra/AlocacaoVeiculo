using System.ComponentModel.DataAnnotations;

namespace AlocacaoVeiculosAssistencia.Application.DTOs.Veiculos
{
    public class VeiculosCreateDto
    {
        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A placa é obrigatória.")]
        [RegularExpression(
            @"^[A-Za-z]{3}[0-9]{4}$|^[A-Za-z]{3}[0-9][A-Za-z][0-9]{2}$",
            ErrorMessage = "A placa deve estar no formato AAA1234 ou AAA1A23.")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "O grupo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O grupo é inválido.")]
        public int GrupoId { get; set; }

    }
}
