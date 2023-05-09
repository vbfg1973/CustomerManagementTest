using CustomerManagement.Api.Extensions;
using CustomerManagement.Api.Support;
using CustomerManagement.Data;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.ConfigureApp();
builder.ConfigureLogging();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerAndConfig();
builder.Services.AddVersioning();
builder.Services.AddDatabase(appSettings);
builder.Services.AddMediatR(ApiAssemblyReference.Assembly);

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
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();