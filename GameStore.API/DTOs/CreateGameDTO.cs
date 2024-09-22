namespace GameStore.API.DTOs;

public record class CreateGameDTO(
    string Name, 
    string Genre, 
    decimal Price,
    DateOnly ReleaseDate
);
