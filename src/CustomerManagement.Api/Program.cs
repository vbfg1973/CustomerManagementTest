using CustomerManagement.Api.Extensions;
using CustomerManagement.Api.Support;
using CustomerManagement.Data;
using CustomerManagement.Domain.Support;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.ConfigureApp();
builder.ConfigureLogging();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerAndConfig();
builder.Services.AddVersioning();
builder.Services.AddDatabase(appSettings);
// builder.Services.AddHealthChecks();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(DomainAssemblyReference.Assembly));
builder.Services.AddAutoMapper(DomainAssemblyReference.Assembly, ApiAssemblyReference.Assembly);
builder.Services.AddConfiguredHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MigrateDatabase<CustomerManagementContext>();
app.UseCorrelationId();
app.UseCustomExceptionHandler();
app.UseSerilogRequestLogging();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    AllowCachingResponses = false
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();