using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestApp2.Data;
using System;
using System.Linq;
using TestApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TestApp2.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ClientsContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ClientsContext>>()))
        {
            // Look for any movies.
            if (context.Client.Any())
            {
                return;
            }
            context.Client.AddRange(
                new Client
                {
                    INN = "1234567890",
                    Name = "ООО \"Какие Люди\"",
                    ClientType = "Юридическое лицо",
                    CreatedAt = DateTime.Parse("2023-06.01"),
                    LastUpdatedAt = DateTime.Parse("2023-06.05")
                },
                new Client
                {
                    INN = "1122334455",
                    Name = "ИП Иванов Иван Иванович",
                    ClientType = "Индивидуальный предприниматель",
                    CreatedAt = DateTime.Parse("2023-06.05"),
                    LastUpdatedAt = DateTime.Parse("2023-06.10")
                },
                new Client
                {
                    INN = "2233445566",
                    Name = "ООО \"ООО\"",
                    ClientType = "Юридическое лицо",
                    CreatedAt = DateTime.Parse("2023-06.10"),
                    LastUpdatedAt = DateTime.Parse("2023-06.15")
                },
                new Client
                {
                    INN = "4455667788",
                    Name = "ИП Максименко Максим Максимович",
                    ClientType = "Индивидуальный предприниматель",
                    CreatedAt = DateTime.Parse("2023-06.15"),
                    LastUpdatedAt = DateTime.Parse("2023-06.20")
                }
            );
            context.SaveChanges();
        }
    }
}