using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _grassTile, _mountainTile, _riverTile;

    [SerializeField] private Transform _cam;

    public Dictionary<Vector2, Tile> _tiles;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateGrid() {
        _tiles = new Dictionary<Vector2, Tile>();
        int verticalRiver = 0;
        int horizontalRiver = 0;
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                if (!_tiles.ContainsKey(new Vector2(x, y))) {
                    int randNum = Random.Range(0, 30);
                    if (randNum == 0) {
                        if (x < _width - 4 && verticalRiver < 2) {
                            verticalRiver++;
                            for (int i = 0; i < Random.Range(3, 5); i++)
                            {
                                var randomTile = _riverTile;
                                var spawnedTile = Instantiate(randomTile, new Vector3(x+i, y), Quaternion.identity);
                                spawnedTile.name = $"Tile {x + i} {y}";
                                spawnedTile.gridLocation = new Vector2(x + i, y);

                                spawnedTile.Init(x + i, y);

                                _tiles[new Vector2(x + i, y)] = spawnedTile;
                            }
                        } else if(y < _height - 4 && horizontalRiver < 2) {
                            horizontalRiver++;
                            for (int i = 0; i < Random.Range(3, 5); i++)
                            {
                                var randomTile = _riverTile;
                                var spawnedTile = Instantiate(randomTile, new Vector3(x, y + i), Quaternion.identity);
                                spawnedTile.name = $"Tile {x} {y + i}";
                                spawnedTile.gridLocation = new Vector2(x, y + i);

                                spawnedTile.Init(x, y + i);

                                _tiles[new Vector2(x, y + i)] = spawnedTile;
                            }
                        } else {
                            var randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
                            var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                            spawnedTile.name = $"Tile {x} {y}";
                            spawnedTile.gridLocation = new Vector2(x, y);

                            spawnedTile.Init(x, y);

                            _tiles[new Vector2(x, y)] = spawnedTile;

                        }
                    }else {
                        var randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
                        var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                        spawnedTile.name = $"Tile {x} {y}";
                        spawnedTile.gridLocation = new Vector2(x, y);

                        spawnedTile.Init(x, y);

                        _tiles[new Vector2(x, y)] = spawnedTile;
                    }
                }
            }
        }






        _cam.transform.position = new Vector3((float)_width/2 - 0.5f, (float)_height/2 - 0.5f, -10);

        GameManager.Instance.ChangeState(GameState.SpawnHeros);
    }

    public Tile GetHeroSpawnTile()
    {
        return _tiles.Where(t => t.Key.x < _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetEnemySpawnTile()
    {
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }

    public List<Tile> GetNeighbourTiles(Tile currentTile, bool withEnemies) {
        var tiles = GridManager.Instance._tiles;

        List<Tile> neighbours = new List<Tile>();


        //right
        Vector2 locationToCheck = new Vector2(
            currentTile.gridLocation.x + 1,
            currentTile.gridLocation.y
            );
        if (tiles.ContainsKey(locationToCheck) && (withEnemies ? tiles[locationToCheck]._isWalkable : tiles[locationToCheck].Walkable)) {
            neighbours.Add(tiles[locationToCheck]);
        }

        //left
        locationToCheck = new Vector2(
            currentTile.gridLocation.x - 1,
            currentTile.gridLocation.y
            );
        if (tiles.ContainsKey(locationToCheck) && (withEnemies ? tiles[locationToCheck]._isWalkable : tiles[locationToCheck].Walkable)) {
            neighbours.Add(tiles[locationToCheck]);
        }

        //top
        locationToCheck = new Vector2(
            currentTile.gridLocation.x,
            currentTile.gridLocation.y + 1
            );
        if (tiles.ContainsKey(locationToCheck) && (withEnemies ? tiles[locationToCheck]._isWalkable : tiles[locationToCheck].Walkable)) {
            neighbours.Add(tiles[locationToCheck]);
        }

        //bottom
        locationToCheck = new Vector2(
            currentTile.gridLocation.x,
            currentTile.gridLocation.y - 1
            );
        if (tiles.ContainsKey(locationToCheck) && (withEnemies ? tiles[locationToCheck]._isWalkable : tiles[locationToCheck].Walkable)) {
            neighbours.Add(tiles[locationToCheck]);
        }

        return neighbours;
    }
}
