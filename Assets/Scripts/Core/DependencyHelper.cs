using System;
using MergeMechanic.Core;
using MergeMechanic.Core.Board;
using MergeMechanic.Core.Recipe;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

public sealed class DependencyHelper
{
    private static readonly Lazy<DependencyHelper> lazy = new Lazy<DependencyHelper>(() => new DependencyHelper());
    private readonly ServiceProvider _serviceProvider;

    public static T GetRequiredService<T>() where T : notnull
        => lazy.Value._serviceProvider.GetRequiredService<T>();

    private DependencyHelper()
    {
        Debug.Log("In Constructor");
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<IGameObjectWrapper, GameObjectWrapper>();
        serviceCollection.AddSingleton<ITileTracker, TileTracker>();

        serviceCollection.AddTransient(typeof(IEventPublisher<>), typeof(EventPublisher<>));
        // serviceCollection.AddTransient(typeof(IEventListener<TileMergedEvent>), typeof(TileMerger));

        serviceCollection.AddTransient<IBoardGenerator, BoardGenerator>();
        serviceCollection.AddTransient<IGridHelper, GridHelper>();
        serviceCollection.AddTransient<IRecipeMerger, RecipeMerger>();
        
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }
}
