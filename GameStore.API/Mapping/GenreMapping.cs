using System;
using GameStore.API.DTOs;
using GameStore.API.Entities;

namespace GameStore.API.Mapping;

public static class GenreMapping
{
    public static GenreDTO ToDTO(this Genre genre) {
        return new GenreDTO(genre.Id, genre.Name);
    }
}
