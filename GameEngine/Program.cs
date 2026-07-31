using GameEngine;
using GameEngine.Entities;
using GameEngine.Map;
using GameEngine.Map.Tiles;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Logger logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .WriteTo.File(
                "logs/log.txt",
                rollingInterval: RollingInterval.Day
            )
            .WriteTo.Debug()
            .CreateLogger();

        ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddSerilog(logger, dispose: true);
        });

        Dictionary<TileType, TileDefinition> tileDefinitions = await TileDefinitionHelper.GetDefinitions("./Resources/TileDefinitions.json");
        Dictionary<EntityType, EntityDefinition> entityDefinitions = await EntityDefinitionHelper.GetDefinitions("./Resources/EntityDefinitions.json");
        MapDto map = await MapHelper.GetMap("./Resources/Map.json");

        logger.Information("========= STARTUP =========");
        
        IGameEngine gameEngine = new GameEngine.GameEngine(
            tileDefinitions,
            entityDefinitions,
            map,
            loggerFactory.CreateLogger<GameEngine.GameEngine>()
        );

        gameEngine.Start();

        logger.Information("======== CLEARDOWN ========");
    }
}