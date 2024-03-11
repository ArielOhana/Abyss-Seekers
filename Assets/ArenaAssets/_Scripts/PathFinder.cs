using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    public List<Tile> FindPath(Tile start, Tile end, bool toAttack) {
        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(start);
        while (openList.Count > 0) {
        
            Tile currentTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            if(currentTile == end) {
                return GetFinishedList(start, end);
            }

            var neighbourTiles = GridManager.Instance.GetNeighbourTiles(currentTile, toAttack);

            foreach(var neighbour in  neighbourTiles) {
                if((toAttack ? !neighbour._isWalkable : !neighbour.Walkable) || closedList.Contains(neighbour)) {
                    continue;
                }

                neighbour.G = GetManhattenDistance(start, neighbour);
                neighbour.H = GetManhattenDistance(end, neighbour);

                neighbour.previous = currentTile;
                if (!openList.Contains(neighbour)) {
                    openList.Add(neighbour);
                }
            }
        }
        return new List<Tile>();
    }

    private List<Tile> GetFinishedList(Tile start, Tile end) {
        List<Tile> finishedList = new List<Tile>();

        Tile currentTile = end;

        while(currentTile != start) {
            finishedList.Add(currentTile);
            currentTile = currentTile.previous;
        }

        finishedList.Reverse();

        return finishedList;
    }

    private int GetManhattenDistance(Tile start, Tile neighbour) {
        return (int)(Mathf.Abs(start.gridLocation.x - neighbour.gridLocation.x) + Mathf.Abs(start.gridLocation.y - neighbour.gridLocation.y));
    }
}
