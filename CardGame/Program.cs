using Infrastruct;
using Application;
using Application.Middleware;
using FluentValidation.AspNetCore;
using Domain;
using Application.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidation(x =>
{
    x.ImplicitlyValidateChildProperties = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("Client", builder =>
        builder.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://127.0.0.1:5500")
    );
});

//Dependency injection configuration
builder.Services
    .AddDomain()
    .AddInfrastructure(builder.Configuration)
    .AddAplication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Client");

//Middlewares
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();

app.UseRouting();

app.MapHub<MainHub>("/main");
app.MapHub<GameRoomHub>("/room");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
