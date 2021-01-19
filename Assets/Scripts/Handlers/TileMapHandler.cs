using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
[RequireComponent(typeof(TilemapCollider2D))]
public class TileMapHandler : MonoBehaviour
{
    public TileBase StoneTile;

    public int NumTileToSpawn;

    private int _tileIterator;

    private Tilemap _tilemap;
    private TilemapCollider2D _tilemapCollider;
    private List<ContactPoint2D> _contactPoints;

    private float _camHorizontalBounds;

    private void Awake()
    {
        if(GetComponent<Tilemap>() != null)
        {
            _tilemap = GetComponent<Tilemap>();
            _tilemapCollider = GetComponent<TilemapCollider2D>();
        }
        else
        {
            Debug.LogWarning("Failed to get Tilemap on " + gameObject.name);
        }

        _tileIterator = 0;
        _contactPoints = new List<ContactPoint2D>();
    }

    private void Start()
    {
        // Inverse because we want it to move towards the player (left side of display)
        _camHorizontalBounds = Camera.main.orthographicSize * Screen.width / Screen.height * -1;

        StartCoroutine(AddTiles());
        
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {    
        var newPos = Vector3.MoveTowards(transform.position, new Vector3(_camHorizontalBounds, 0, 0), 1.5f * Time.deltaTime);
        transform.position = newPos;
    }

    private IEnumerator AddTiles()
    {
        for (_tileIterator = 0; _tileIterator < NumTileToSpawn; _tileIterator++)
        {
            _tilemap.SetTile(new Vector3Int(_tileIterator, 0, 0), StoneTile);
            yield return null;
        }
    }
    
}
