using System;
using System.Reflection.Metadata.Ecma335;
using GameStore.API.Data;
using GameStore.API.DTOs;
using GameStore.API.Entities;
using GameStore.API.Mapping;

namespace GameStore.API.Endpoints;

public static class GamesEndpoints
{
    const string getGameEndpointName = "GetGame";

    private static readonly List<GameSummaryDTO> games = [
        new (
            1,
            "Skyrim",
            "Adventure",
            40M,
            new DateOnly(2012, 7, 21)
        ),
        new (
            2,
            "Asphalt 3",
            "Racing",
            0M,
            new DateOnly(2011, 2, 27)
        ),
        new (
            3,
            "League Of Legends",
            "Self Harm",
            999999M,
            new DateOnly(2009, 3, 15)
        ),
        new (
            4,
            "Assassin's Creed",
            "Fun",
            0M,
            new DateOnly(2007, 5, 13)
        ),
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app) {

        var group = app.MapGroup("/games").WithParameterValidation();

        // GET /games
        group.MapGet("/", () => games);

        // GET /games/1
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) => {
            Game? game = dbContext.Games.Find(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDTO());
        })
        .WithName(getGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDTO newGame, GameStoreContext dbContext) => {
            Game game = newGame.ToEntity();
            
            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(getGameEndpointName, new { id = game.Id }, game.ToGameDetailsDTO());
        });

        // PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDTO updatedGame) => {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1) {
                return Results.NotFound();
            }

            games[index] = new GameSummaryDTO(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id) => {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
