namespace AlocacaoVeiculosAssistencia.Domain.Entities;

public class VeiculosAssistencia
{
    public int Id { get; set; }

    public int VeiculoId { get; set; }

    public Veiculos Veiculo { get; set; } = null!;

    public int PlanoId { get; set; }


    public PlanosAssistencia Plano { get; set; } = null!;
    
}