using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeFinder
{
    public List<Tile> GetTilesInRange(Tile startingTile, int range) {
        var inRangeTiles = new List<Tile>();
        int stepCount = 0;

        inRangeTiles.Add(startingTile);

        var tileForPreviousStep = new List<Tile>();
        tileForPreviousStep.Add(startingTile);

        while (stepCount < range) {
            var surroundingTiles = new List<Tile>();

            foreach (var item in tileForPreviousStep) {
                surroundingTiles.AddRange(GridManager.Instance.GetNeighbourTiles(item, false));
            }

            inRangeTiles.AddRange(surroundingTiles);
            tileForPreviousStep = surroundingTiles.Distinct().ToList();
            stepCount++;
        }

        return inRangeTiles.Distinct().ToList();
    }

    public List<Tile> GetTilesInAttackRange(Tile startingTile, int range, int attackRange) {
        var inAttackRange = new List<Tile>();
        int stepCount = 0;

        inAttackRange.Add(startingTile);

        var tileForPreviousStep = new List<Tile>();
        tileForPreviousStep.Add(startingTile);

        while (stepCount < (range + attackRange)) {
            var surroundingTiles = new List<Tile>();

            foreach (var item in tileForPreviousStep) {
                surroundingTiles.AddRange(GridManager.Instance.GetNeighbourTiles(item, true));
            }

            inAttackRange.AddRange(surroundingTiles);
            tileForPreviousStep = surroundingTiles.Distinct().ToList();
            stepCount++;
        }

        return inAttackRange.Distinct().ToList();
    }
}
