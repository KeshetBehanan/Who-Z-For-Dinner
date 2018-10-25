using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TileMap : MonoBehaviour
{
    private struct TilePostion
    {
        public TilePostion(int z, int x)
        {
            Z = z;
            X = x;
        }

        public int Z { get; private set; }
        public int X { get; private set; }
    }

    public static GameObject[,] tiles;
    private int activeTiles;

    public GameObject tilePrefab;
    public Transform player;
    public int size = 9;

    private void Awake()
    {
        tiles = new GameObject[size, size];
        activeTiles = size * size;
    }

    private void Start()
    {
        for(int z = 0; z < size; z++)
        {
            for(int x = 0; x < size; x++)
            {
                tiles[x, z] = GameObject.Instantiate(tilePrefab, new Vector3(x, 0, z), Quaternion.identity, transform);
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            Increase();
        }
    }

    public void Increase()
    {
        var tilePos = GetFarthestTileFromPlayer();
        DestroyTile(tiles[tilePos.Z, tilePos.X]);
        tiles[tilePos.Z, tilePos.X] = null;
        activeTiles--;
        if(activeTiles == 0)
        {
            print("Game Over");
        }
    }

    private TilePostion GetFarthestTileFromPlayer()
    {
        float farthestDistance = 0;
        TilePostion farthestTile = new TilePostion();

        for(int z = 0; z < size; z++)
        {
            for(int x = 0; x < size; x++)
            {
                var tile = tiles[z, x];

                if(tile == null)
                {
                    continue;
                }
                var dis = (tile.transform.position - player.position).magnitude;
                if(dis > farthestDistance)
                {
                    farthestDistance = dis;
                    farthestTile = new TilePostion(z, x);
                }
            }
        }

        return farthestTile;
    }

    private void DestroyTile(GameObject tile)
    {
        tile.AddComponent<Rigidbody>();
        Destroy(tile, 4f);
    }
}
