using AlocacaoVeiculosAssistencia.Domain.Entities;

public class EmpresasAssistencia
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Endereco { get; set; } = string.Empty;


    public ICollection<PlanosAssistencia> PlanosAssistencia { get; set; }
        = new List<PlanosAssistencia>();
}