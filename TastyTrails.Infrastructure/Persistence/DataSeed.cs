using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TastyTrails.Application.Common.Interfaces;
using TastyTrails.Domain.Entities;

namespace TastyTrails.Infrastructure.Persistence
{
    public class DataSeed
    {
        private readonly TastyTrailsDbContext _dbContext;
        private readonly ILogger<DataSeed> _logger;

        public DataSeed(TastyTrailsDbContext dbContext, ILogger<DataSeed> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            _logger.LogInformation("Database seeding began");

            await _dbContext.Database.MigrateAsync();

            if (!await _dbContext.Restaurants.AnyAsync())
            {

                var restaurantData = new List<Restaurant>()
                {
                    new()
                    {
                        Name = "Kraljica",
                        Location = "Cacak, Nemanjina 28",
                        Description = "Pizzeria & Caffee",
                        Menu = new List<Menu>()
                        {
                            new()
                            {
                                Name = "Pizza",
                                Description = "Izbor pizza",
                                Items = new List<MenuItem>()
                                {
                                    new()
                                    {
                                        Name = "Gondola",
                                        Description = "(kečap, kačkavalj, kulen, slanina suvi vrat, masline, jaja)",
                                        Price = 970
                                    },
                                    new()
                                    {
                                        Name = "Quattro Formaggi e Prosciutto",
                                        Description = "",
                                        Price = 1340
                                    },
                                    new()
                                    {
                                        Name = "Pizza sa piletinom",
                                        Description = "(preliv, kačkavalj, belo meso, šampinjoni, sveže povrće, pomfrit)",
                                        Price = 1290
                                    }
                                }
                            },
                            new()
                            {
                                Name = "Paste",
                                Description = "Najfiniji izbor pasti",
                                Items = new List<MenuItem>()
                                {
                                    new()
                                    {
                                        Name = "Tagliatelle u sosu sa mešanim pečurkama",
                                        Description = "(lisičarka, vrganj, tartufi ulje)",
                                        Price = 970
                                    },
                                    new()
                                    {
                                        Name = "Gnocchi Quattro Formaggi",
                                        Description = "(njoke, masline, gorgonzola, mozzarella, ementaler, kačkavalj, parmezan, pesto sos)",
                                        Price = 950
                                    },
                                    new()
                                    {
                                        Name = "Spaghetti Bolognese",
                                        Description = "(paradajz sos, juneće mleveno meso, parmezan)",
                                        Price = 960
                                    }
                                }
                            },
                            new()
                            {
                                Name = "Specials",
                                Description = "Specijaliteti nase kuhinje",
                                Items = new List<MenuItem>()
                                {
                                    new()
                                    {
                                        Name = "Biftek u peper sosu",
                                        Description = "(biftek, grilovano povrće)",
                                        Price = 2700
                                    },
                                    new()
                                    {
                                        Name = "Piletina na Thai način",
                                        Description = "(piletina, šampinjoni, šargarepa, paprika, soja sos, indijski orah, ananas)",
                                        Price = 1280
                                    },
                                    new()
                                    {
                                        Name = "Pileći file u sosu od parmezana",
                                        Description = "(pileći file, parmezan, tagliatelle 4 sira)",
                                        Price = 1250
                                    }
                                }
                            },
                            new()
                            {
                                Name = "Alkoholna pica",
                                Description = "",
                                Items = new List<MenuItem>()
                                {
                                    new()
                                    {
                                        Name = "Pivo Balanc",
                                        Description = "",
                                        Price = 270
                                    },
                                    new()
                                    {
                                        Name = "Pranjanska Rakija - Sljiva",
                                        Description = "Rakijada Pranjani Limited Edition",
                                        Price = 666
                                    },
                                    new()
                                    {
                                        Name = "Koktel Negroni",
                                        Description = "(džin, crveni vermut, Campari)",
                                        Price = 694
                                    }
                                }
                            }
                        }
                    }
                };

                await _dbContext.Restaurants.AddRangeAsync(restaurantData);
                await _dbContext.SaveChangesAsync();
            }

            _logger.LogInformation("Database seeding ended");
        }
    }
}
