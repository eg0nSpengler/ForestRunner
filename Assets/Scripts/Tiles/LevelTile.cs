using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTile : MonoBehaviour, ITile
{
    public Transform TileStartPos;
    public Transform TileEndPos;

    public bool IsInstantKill { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake()
    {
        if (!TileStartPos || !TileEndPos)
        {
            Debug.LogWarning(gameObject.name + " is missing a TileStart/TileEnd reference!");
        }
    }

    public void SetTileActive()
    {
        
    }

    public void SetTileInactive()
    {

    }

}
