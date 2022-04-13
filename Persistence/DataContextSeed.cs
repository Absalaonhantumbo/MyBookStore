using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Domain;
using Microsoft.Extensions.Logging;

namespace Persistence
{
    public class DataContextSeed 
    {
        public static async Task SeedAsync(DataContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ClientTypes.Any())
                {
                    var clientTypeData = File.ReadAllText("../Persistence/SeedData/clientType.json");
                    var clientTypes = JsonSerializer.Deserialize<List<ClientType>>(clientTypeData);

                    foreach (var clientType in clientTypes)
                    {
                        await context.AddAsync(clientType);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<DataContext>();
                logger.LogError(e, e.Message);
            }
        }
    }
}