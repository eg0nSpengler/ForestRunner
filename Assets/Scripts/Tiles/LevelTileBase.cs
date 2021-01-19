using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class LevelTileBase : TileBase
{
    public Sprite TileSprite;
    public GameObject TilePrefab;
    public Vector3Int TilePosition;

    public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
    {
        position = TilePosition;
        return base.GetTileAnimationData(position, tilemap, ref tileAnimationData); 
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        position = TilePosition;
        base.RefreshTile(position, tilemap);
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        position = TilePosition;
        tileData.sprite = TileSprite;
        tileData.gameObject = TilePrefab;
        base.GetTileData(position, tilemap, ref tileData);
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        position = TilePosition;
        go = TilePrefab;
        return StartUp(position, tilemap, go);
    }
}
