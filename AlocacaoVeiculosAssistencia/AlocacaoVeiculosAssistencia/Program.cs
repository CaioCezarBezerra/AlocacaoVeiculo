using AlocacaoVeiculosAssistencia.Application.Interfaces.Repository;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Services;
using AlocacaoVeiculosAssistencia.Application.Services;
using AlocacaoVeiculosAssistencia.Data.Repository;

using AlocacaoVeiculosAssistencia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    Environment.GetEnvironmentVariable(
        "ConnectionStrings__DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "A variável ConnectionStrings__DefaultConnection não foi configurada.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));



builder.Services.AddScoped<IEmpresaService, EmpresaAssistenciaService>();
builder.Services.AddScoped<IPlanoService, PlanoAssistenciaService>();
builder.Services.AddScoped<IVeiculoService, VeiculoService>();
builder.Services.AddScoped<IVeiculoAssistenciaService, VeiculoAssistenciaService>();
builder.Services.AddScoped<IGrupoVeiculoService, GrupoVeiculoService>();


builder.Services.AddScoped<IEmpresasAssistenciaRepository, EmpresaAssistenciaRepository>();
builder.Services.AddScoped<IPlanoAssistenciaRepository, PlanoAssistenciaRepository>();
builder.Services.AddScoped<IVeiculoRepository, VeiculosRepository>();
builder.Services.AddScoped<IVeiculoAssistenciasRepository, VeiculoAssistenciaRepository>();
builder.Services.AddScoped<IGrupoVeiculosRepository, GruposVeiculosRepository>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();