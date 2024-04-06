using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

configuration.AddEnvironmentVariables();
configuration.AddJsonFile("ocelot.json");
configuration.AddJsonFile("appsettings.json");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway"));
}

app.UseHsts();
app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseRouting();

app.UseOcelot().Wait();

app.Run();