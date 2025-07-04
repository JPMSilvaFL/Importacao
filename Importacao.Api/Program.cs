using System.Data;
using System.Text.Json.Serialization;
using Importacao.Api.Middlewares;
using Importacao.Application;
using Importacao.Application.Interfaces;
using Importacao.Application.Services;
using Importacao.Infrastructure.Interfaces;
using Importacao.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

var connectionName = "DefaultConnection";
var connectionString = builder.Configuration.GetConnectionString(connectionName)!;

ConfigureMvc(builder);
ConfigureServices(builder);

var app = builder.Build();

LoadConfiguration(app);

app.UseCors("MyCorsPolicy");
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();

void ConfigureMvc(WebApplicationBuilder builderMvc) {
	builderMvc.Services.AddControllers().ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; }).AddJsonOptions(x => {
		x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
		x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
	});
}

void ConfigureServices(WebApplicationBuilder builderServices) {
	builderServices.Services.AddCors(options => {
		options.AddPolicy("MyCorsPolicy", // Nome da política
			policy => {
				policy.WithOrigins("http://localhost:5173"); // Origens permitidas
				policy.WithMethods("GET", "POST", "PUT", "DELETE"); // Métodos HTTP permitidos
				policy.WithHeaders("Content-Type", "Authorization"); // Headers permitidos
				// policy.AllowAnyOrigin(); // Permite todas as origens (não recomendado para produção)
				// policy.AllowAnyMethod(); // Permite todos os métodos (não recomendado para produção)
				// policy.AllowAnyHeader(); // Permite todos os headers (não recomendado para produção)
				policy.AllowCredentials(); // Permite credenciais (cookies, autenticação)
			});
	});

	builderServices.Services.AddEndpointsApiExplorer();
	builderServices.Services.AddSwaggerGen();

	builderServices.Services.AddScoped<IPersonService, PersonService>();

	builder.Services.AddTransient<IDbConnection>(sp => new SqlConnection(connectionString));

	builderServices.Services.AddScoped<IPersonRepository, PersonRepository>();
}


void LoadConfiguration(WebApplication appConfig) {
	Configuration.ConnectionString = connectionString;
}