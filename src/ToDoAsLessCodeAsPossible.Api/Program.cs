using ToDoAsLessCodeAsPossible.Api;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.ErrorHandling;
using ToDoAsLessCodeAsPossible.Infrastructure;
using ToDoAsLessCodeAsPossible.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();
builder.Services.AddExceptionToResponseMapping();
builder.Services.AddDefaultExceptionToResponseMapper();

var app = builder.Build();
DatabaseCreator.CreateDatabaseSchema(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.RegisterEndpoints();
app.UseExceptionToResponseMapping();

app.Run();