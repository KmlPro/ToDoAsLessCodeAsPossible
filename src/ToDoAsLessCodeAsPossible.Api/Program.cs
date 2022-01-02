using ToDoAsLessCodeAsPossible.Api;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests;
using ToDoAsLessCodeAsPossible.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure();
builder.Services.AddRequestToUseCaseMapping(assembly);

var app = builder.Build();

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

app.Run();