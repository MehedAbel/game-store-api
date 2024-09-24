using GameStore.API.Data;
using GameStore.API.DTOs;
using GameStore.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = "Data Source = GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
