﻿using System;
using Moq;
using VRageMath;
using World.GameActions;
using World.GameActors;
using World.GameActors.Tiles;
using Xunit;
using World.ToyWorldCore;

namespace ToyWorldTests.World
{
    public class GameActionsTests
    {
        [Fact]
        public void PickupResolveAddsToInventory()
        {
            Mock<IAtlas> atlas = new Mock<IAtlas>();
            Mock<GameActor> actor = new Mock<GameActor>();
            Mock<ICanPick> picker = actor.As<ICanPick>();
            picker.Setup(x => x.AddToInventory(It.IsAny<IPickable>()));

            Mock<GameActor> targetActor = new Mock<GameActor>();
            Mock<IPickable> target = targetActor.As<IPickable>();

            PickUp pickUp = new PickUp(actor.Object);

            // Act
            pickUp.Resolve(new GameActorPosition(targetActor.Object, new Vector2()), atlas.Object, It.IsAny<ITilesetTable>());

            // Assert
            picker.Verify(x => x.AddToInventory(It.IsAny<IPickable>()));
        }
    }
}
