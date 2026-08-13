namespace AlocacaoVeiculosAssistencia.Domain.Entities;

public class PlanosAssistencia
{
    public int Id { get; set; }

    public string Plano { get; set; } = string.Empty;

    public int EmpresaId { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public decimal ValorCobertura { get; set; }
    public EmpresasAssistencia Empresa { get; set; } = null!;


    public ICollection<VeiculosAssistencia> Veiculos { get; set; }
        = new List<VeiculosAssistencia>();
}