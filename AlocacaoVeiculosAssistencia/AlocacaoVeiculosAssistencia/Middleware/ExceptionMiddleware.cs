using System.Net;
using AlocacaoVeiculosAssistencia.Application.Exceptions;

namespace AlocacaoVeiculosAssistencia.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Recurso não encontrado."
                );

                await ResponderErroAsync(
                    context,
                    HttpStatusCode.NotFound,
                    ex.Message
                );
            }
            catch (ConflictException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Conflito de regra de negócio."
                );

                await ResponderErroAsync(
                    context,
                    HttpStatusCode.Conflict,
                    ex.Message
                );
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Dados inválidos."
                );

                await ResponderErroAsync(
                    context,
                    HttpStatusCode.BadRequest,
                    ex.Message
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro não tratado na API."
                );

                await ResponderErroAsync(
                    context,
                    HttpStatusCode.InternalServerError,
                    "Ocorreu um erro interno no servidor."
                );
            }
        }


        private static async Task ResponderErroAsync(
            HttpContext context,
            HttpStatusCode statusCode,
            string mensagem)
        {
            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                (int)statusCode;

            await context.Response.WriteAsJsonAsync(
                new
                {
                    message = mensagem
                }
            );
        }
    }
}