﻿using World.GameActions;
using World.ToyWorldCore;
using World.WorldInterfaces;

namespace World.GameActors.Tiles
{
    /// <summary>
    /// </summary>
    public interface IAutoupdateable
    {
        Tile Update(IAtlas atlas);

        int NextUpdateAfter { get; }
    }

    /// <summary>
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Method is called when something apply GameAction on this object.
        /// </summary>
        void ApplyGameAction(IAtlas atlas, GameAction gameAction, TilesetTable tilesetTable = null);
    }
}