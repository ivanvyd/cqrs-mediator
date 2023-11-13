using CqrsMediator.Infrastructure;
using CqrsMediator.Infrastructure.Commands;
using CqrsMediator.Infrastructure.Queries;
using CqrsMediator.WebApi.Users;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add custom CQRS and Mediator services from the current assembly to the service collection.
var currentAssembly = Assembly.GetExecutingAssembly();
builder.Services.AddCustomCqrsAndMediator(currentAssembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Map demo endpoints
app.MapGet("/users", async (IQueryTransmitter queryTransmitter) =>
{
    var usersNamesByIds = await queryTransmitter.TransmitAsync<Dictionary<Guid, string>>();

    return Results.Ok(usersNamesByIds);
})
.WithSummary("Get Users")
.Produces<Dictionary<Guid, string>>(StatusCodes.Status200OK)
.WithOpenApi();

app.MapGet("/users/{userId}", async ([FromServices] IQueryTransmitter queryTransmitter, [FromQuery] Guid userId) =>
{
    var userName = await queryTransmitter.TransmitAsync<GetUserNameByIdQuery, string>(new GetUserNameByIdQuery
    {
        UserId = userId,
    });

    return Results.Ok(userName);
})
.WithSummary("Get User Name by Id")
.Produces<string>(StatusCodes.Status200OK)
.WithOpenApi();

app.MapDelete("/users/{userId}", async ([FromServices] ICommandTransmitter commandTransmitter, [FromQuery] Guid userId) =>
{
    await commandTransmitter.TransmitAsync(new DeleteUserByIdCommand
    {
        UserId = userId,
    });

    return Results.NoContent();
})
.WithSummary("Delete User by Id")
.Produces(StatusCodes.Status204NoContent)
.WithOpenApi();

app.Run();