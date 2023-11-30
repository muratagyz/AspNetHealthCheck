using HealthCheck.Api.Context;
using HealthCheck.Api.Services;
using HealthChecks.UI.Client;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<HealthContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HealthContext"));
});

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services
    .AddHealthChecks()
    .AddSqlServer(connectionString: builder.Configuration.GetConnectionString("HealthContext"));

builder.Services
    .AddHealthChecksUI(options =>
    {
        options.AddHealthCheckEndpoint("api", "/health");
        options.SetEvaluationTimeInSeconds(5);
        options.SetMinimumSecondsBetweenFailureNotifications(10);
    })
    .AddInMemoryStorage();
    

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecksUI(options =>
{
    options.UIPath = "/healthchecks-ui";
    options.ApiPath = "/health-ui-api";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();