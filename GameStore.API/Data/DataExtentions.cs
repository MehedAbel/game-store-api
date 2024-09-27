using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public static class DataExtentions
{
    public static void MigrateDb(this WebApplication app) {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }
}
