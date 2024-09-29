using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public static class DataExtentions
{
    public static async Task MigrateDbAsync(this WebApplication app) {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        await dbContext.Database.MigrateAsync();
    }
}
