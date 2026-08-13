using System.ComponentModel.DataAnnotations;


namespace AlocacaoVeiculosAssistencia.Application.DTOs.GrupoVeiculos
{
    public class GrupoVeiculosCreateDtos
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

    }
}

