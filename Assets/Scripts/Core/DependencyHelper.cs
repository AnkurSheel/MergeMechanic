using System;
using MergeMechanic.Core;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

public sealed class DependencyHelper
{
    private static readonly Lazy<DependencyHelper> lazy = new Lazy<DependencyHelper>(() => new DependencyHelper());
    private readonly ServiceProvider _serviceProvider;

    public static T GetRequiredService<T>()
        => lazy.Value._serviceProvider.GetRequiredService<T>();

    private DependencyHelper()
    {
        Debug.Log("In Constructor");
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton(typeof(IEventPublisher<>), typeof(EventPublisher<>));

        serviceCollection.AddSingleton<IBoardGenerator, BoardGenerator>();
        serviceCollection.AddSingleton<IGridHelper, GridHelper>();
        serviceCollection.AddSingleton<IGameObjectWrapper, GameObjectWrapper>();
        serviceCollection.AddSingleton<ITileTracker, TileTracker>();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }
}
