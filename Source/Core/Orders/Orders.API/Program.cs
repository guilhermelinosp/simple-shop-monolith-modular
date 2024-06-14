using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Order.Infrastructure;
using Orders.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;

// Register custom services.
services
	.AddRedisCache(configuration)
	.AddMessageBus(configuration)
	.AddMongo(configuration)
	.AddRepositories()
	.AddHandlers()
	.AddSubscribers()
	.AddConsulConfig(configuration);

// Register framework services.
services.AddHttpClient();
services.AddControllers();

// Configure Swagger/OpenAPI.
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "AwesomeShop.Services.Orders.Api",
		Version = "v1"
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI(c =>
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "AwesomeShop.Services.Orders.Api v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

// Use Consul for service discovery.
app.UseConsul();

app.MapControllers();  // Top-level route registration

app.Run();