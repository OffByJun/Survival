using AstraNope.Gameplay.Player;
using AstraNope.Gameplay.Input;
using AstraNope.Core.World.Water;
using AstraNope.Core.World.Water.Interfaces;
using AstraNope.Data.Messages;
using AstraNope.Data.Messages.Player;
using AstraNope.Data.Items;
using AstraNope.Contracts;
using AstraNope.Managers;
using AstraNope.Data.Items;
using AstraNope.Data.Structures;
using AstraNope.Data.Vehicles;
using AstraNope.UI.Panels;
using AstraNope.UI.World;
using AstraNope.Data.Vehicles.Core;
using System;
using AstraNope.Core.World.Entities;
using AstraNope.Core.World.Entities.Bridges;
using AstraNope.Core.World.Entities.Creatures;
using AstraNope.Core.World.Entities.Gateways;
using AstraNope.Core.World.Entities.Interfaces;
using AstraNope.Core.World.Entities.Resources;
using MessagePipe;
using VContainer;
using VContainer.Unity;
using UnityEngine;
using AstraNope.Data.Databases;
using WorldBuilder.Runtime.Grid;

namespace AstraNope.Core
{
    public class GLifeTimeScope : LifetimeScope
    {
        [SerializeField] private HarvestToolCatalog harvestToolCatalog;
        [SerializeField] private CreatureColorFoodCatalog creatureColorFoodCatalog;
        [SerializeField] private WorldGridSettings worldGridSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();
            RegisterMessages(builder, options);
            RegisterManagers(builder);
            RegisterPlayer(builder);
            RegisterWorld(builder);
        }

        private static void RegisterMessages(IContainerBuilder builder, MessagePipeOptions options)
        {
            builder.RegisterMessageBroker<GameStateMessage>(options);
            builder.RegisterMessageBroker<InventoryRequestMessage>(options);
            builder.RegisterMessageBroker<InventoryChangedMessage>(options);
            builder.RegisterMessageBroker<InventorySwapMessage>(options);
            builder.RegisterMessageBroker<HotbarSelectionMessage>(options);
            builder.RegisterMessageBroker<CraftReqMessage>(options);
            builder.RegisterMessageBroker<CraftResultMessage>(options);
            builder.RegisterMessageBroker<UIReqMessage>(options);
            builder.RegisterMessageBroker<InteractionUIMessage>(options);
            builder.RegisterMessageBroker<NotificationMessage>(options);
            builder.RegisterMessageBroker<LogCollectionChangedMessage>(options);
            builder.RegisterMessageBroker<BlueprintProgressChangedMessage>(options);
            builder.RegisterMessageBroker<PlayerUIStateMessage>(options);
            builder.RegisterMessageBroker<PlayerVehicleStateMessage>(options);
            builder.RegisterMessageBroker<VehicleControlAssignedMessage>(options);
            builder.RegisterMessageBroker<PlayerMovementMessage>(options);
            builder.RegisterMessageBroker<PlayerStatMessage>(options);
        }

        private static void RegisterManagers(IContainerBuilder builder)
        {
            builder.Register<NotificationService>(Lifetime.Singleton).As<INotificationService>();
            builder.Register<LogCollectionService>(Lifetime.Singleton)
                .As<ILogCollectionService>()
                .As<ILogCollectionReader>()
                .As<ILogCollectionWriter>();
            builder.Register<JsonLogCatalog>(Lifetime.Singleton).As<ILogCatalog>();
            builder.RegisterInstance(CreateBlueprintDatabase());
            builder.Register<BlueprintProgressService>(Lifetime.Singleton)
                .As<IBlueprintProgressService>()
                .As<IBlueprintProgressReader>()
                .As<IBlueprintProgressWriter>();
            builder.Register<ScanRewardService>(Lifetime.Singleton).As<IScanRewardService>();
            builder.RegisterComponentInHierarchy<GameManager>().As<IGameService>().As<IInitializable>();
            builder.RegisterComponentInHierarchy<UIManager>()
                .As<IUIService>()
                .As<IUIPanelNavigator>()
                .As<IInitializable>();
        }

        private static BluePrintDataBase CreateBlueprintDatabase()
        {
            var database = ScriptableObject.CreateInstance<BluePrintDataBase>();
            database.Reload();
            return database;
        }

        private void RegisterPlayer(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<ItemSpawner>()
                .AsSelf()
                .As<IPickupSpawner>();
            builder.RegisterComponentInHierarchy<InventoryController>()
                .As<IInventoryService>()
                .As<IItemCatalog>()
                .As<IEquipmentReader>()
                .As<IInventoryReader>()
                .As<IInventoryWriter>()
                .As<IInventoryActions>()
                .As<IHotbarReader>()
                .As<IHotbarActions>();
            builder.RegisterComponentInHierarchy<BuildingPlacementController>()
                .As<IBuildingPlacementService>()
                .As<IBuildSelectionReader>();
            builder.RegisterComponentInHierarchy<PlayerContext>()
                .As<IPlayerContext>()
                .As<IPlayerTransformProvider>();
            builder.RegisterComponentInHierarchy<VehicleInjector>();
            builder.RegisterComponentInHierarchy<VehicleSpawner>()
                .AsSelf()
                .As<IVehicleSpawner>();
            builder.RegisterComponentInHierarchy<InputHandler>()
                .As<IInputService>()
                .As<IMovementInput>()
                .As<IInteractionInput>()
                .As<IVehicleInput>()
                .As<IUIInput>()
                .As<IHotbarInput>()
                .As<IHeldItemInput>();
            builder.RegisterComponentInHierarchy<CraftController>()
                .As<ICraftService>()
                .As<ICraftingService>();
            builder.RegisterComponentInHierarchy<WorkbenchPanel>();
            builder.RegisterComponentInHierarchy<SubmarineFabricatorPanel>();
            builder.RegisterComponentInHierarchy<BlueprintPanel>();
        }

        private void RegisterWorld(IContainerBuilder builder)
        {
            if (harvestToolCatalog == null)
                throw new InvalidOperationException(
                    "HarvestToolCatalog is required on GLifeTimeScope. Run the WorldBuilder resource setup or assign it explicitly.");
            builder.RegisterInstance(harvestToolCatalog);
            builder.Register<DotsWorldResourceGateway>(Lifetime.Singleton).As<IWorldResourceGateway>();
            builder.Register<InventoryHarvestToolSelector>(Lifetime.Singleton).As<IHarvestToolSelector>();
            builder.Register<DotsResourceInteractionService>(Lifetime.Singleton).As<IResourceInteractionService>();
            builder.RegisterEntryPoint<DotsResourceInventoryBridge>();

            builder.Register<DotsWorldCreatureGateway>(Lifetime.Singleton)
                .As<IWorldCreatureGateway>()
                .As<ICreatureSpawner>()
                .As<IWorldEntityGateway>();
            builder.RegisterInstance(creatureColorFoodCatalog != null
                ? creatureColorFoodCatalog
                : ScriptableObject.CreateInstance<CreatureColorFoodCatalog>());
            builder.Register<InventoryCreatureToolSelector>(Lifetime.Singleton).As<ICreatureToolSelector>();
            builder.Register<DotsCreatureInteractionService>(Lifetime.Singleton).As<ICreatureInteractionService>();
            builder.RegisterEntryPoint<CreaturePlayerFocusBridge>();
            builder.RegisterEntryPoint<CreatureNameplatePresenter>();
            if (worldGridSettings == null)
                throw new InvalidOperationException(
                    "WorldGridSettings is required on GLifeTimeScope to stream world entity regions.");
            builder.RegisterInstance(worldGridSettings);
            builder.RegisterEntryPoint<WorldEntityRegionStreamingBridge>();
            builder.RegisterEntryPoint<EntityManager>()
                .AsSelf()
                .As<IEntityManager>()
                .As<IEntitySpawnService>()
                .As<IEntityDirectory>()
                .As<IEntityLifecycle>()
                .As<IEntityInteractions>()
                .As<IEntitySettlement>();
            builder.RegisterComponentInHierarchy<WaterQueryService>()
                .As<IWaterQueryService>()
                .As<IWaterQuery>()
                .As<IWaterRegistry>();
        }
    }
}
