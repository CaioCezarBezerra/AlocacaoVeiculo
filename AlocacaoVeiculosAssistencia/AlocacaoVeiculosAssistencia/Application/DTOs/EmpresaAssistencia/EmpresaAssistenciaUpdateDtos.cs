using System.ComponentModel.DataAnnotations;

namespace AlocacaoVeiculosAssistencia.Application.DTOs.Empresa
{
    public class EmpresaAssistenciaUpdateDtos
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public string Endereco { get; set; } = string.Empty;
    }
}
