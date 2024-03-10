using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width = 16;
    [SerializeField] private int _height = 9;

    [SerializeField] private Tile _grassTile, _mountainTile, _lavaTile, _waterTile;

    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Tile currentTile;
                if (x < 4 || (x >= 12 && y >= 4 && y < 8))
                {
                    // Always set the first 4 lines and lines 13-16 to _grassTile
                    currentTile = _grassTile;
                }
                else
                {
                    int randomValue = Random.Range(0, 12);
                    if (randomValue < 4)
                    {
                        currentTile = _grassTile;
                    }
                    else if (randomValue < 8)
                    {
                        currentTile = _grassTile;
                    }
                    else
                    {
                        currentTile = _grassTile;
                    }
                }

                var spawnedTile = Instantiate(currentTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.Init(x, y);
                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            Debug.Log(tile);
            return tile;
        }
        else
        {
            return null;
        }
    }
}
