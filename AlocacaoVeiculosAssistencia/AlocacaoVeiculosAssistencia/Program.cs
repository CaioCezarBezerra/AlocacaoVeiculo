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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddHealthChecks();


var app = builder.Build();
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapHealthChecks("/health");

app.MapControllers();




if (args.Contains("--db-ready"))
{
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    try
    {
        var conectado =
            await context.Database.CanConnectAsync();

        if (conectado)
        {
            Console.WriteLine("Banco de dados disponível.");
            Environment.ExitCode = 0;
        }
        else
        {
            Console.Error.WriteLine(
                "Banco de dados ainda não está disponível.");

            Environment.ExitCode = 1;
        }
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(
            $"Erro ao conectar ao banco: {ex.Message}");

        Environment.ExitCode = 1;
    }

    return;
}




if (args.Contains("--migrate"))
{
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    try
    {
        Console.WriteLine("Aplicando migrations...");

        await context.Database.MigrateAsync();

        Console.WriteLine(
            "Migrations aplicadas com sucesso.");

        Environment.ExitCode = 0;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(
            $"Erro ao aplicar migrations: {ex.Message}");

        Environment.ExitCode = 1;
    }

    return;
}




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



if (!app.Environment.IsEnvironment("Docker"))
{
    app.UseHttpsRedirection();
}


app.UseAuthorization();




app.MapHealthChecks("/health");




app.MapControllers();


app.Run();