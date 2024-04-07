using System;
using AsteroidsSystem;
using Core.ControlSystem;
using GameplaySystem;
using ShipSystem;
using UISystem;
using VContainer.Unity;
using VContainer;

namespace Game
{
    public class BootStarter : LifetimeScope
    {
		protected override void Configure(IContainerBuilder builder)
        {            
            builder.Register<UIController>(Lifetime.Scoped);
            builder.Register<ControlModule>(Lifetime.Scoped);
            builder.Register<GameplayController>(Lifetime.Scoped).As<GameplayController, IStartable>();
            builder.Register<PopupController>(Lifetime.Scoped).As<PopupController, IStartable>();
            builder.Register<AsteroidsController>(Lifetime.Scoped).As<AsteroidsController, IStartable, IDisposable, ITickable>();
            builder.Register<ShipController>(Lifetime.Scoped).As<ShipController, IStartable, IDisposable, ITickable>();

            builder.RegisterComponentInHierarchy<AssetLoader>();
		}       
    }
}