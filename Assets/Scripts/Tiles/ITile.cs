using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An interface for tiles that will be used by the TileTreadmillGenerator
/// </summary>
public interface ITile
{
    void SetTileActive();
    void SetTileInactive();

    /// <summary>
    /// Will touching this tile kill the player <b>instantly?</b>
    /// </summary>
    bool IsInstantKill { get; set; }

}
