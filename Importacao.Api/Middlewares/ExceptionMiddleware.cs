using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Importacao.Api.Middlewares;

public class ExceptionMiddleware {
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionMiddleware> _logger;


	public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger) {
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context) {
		try {
			await _next(context);
		}
		catch (Exception ex) {
			_logger.LogError(ex, ex.Message);
			context.Response.StatusCode = 500;
			await context.Response.WriteAsync("Error!!!");
		}
	}
}