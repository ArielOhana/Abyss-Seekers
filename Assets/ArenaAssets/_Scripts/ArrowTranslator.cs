using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTranslator
{
    public enum ArrowDirection {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4,
        TopRight = 5,
        BottomRight = 6,
        TopLeft = 7,
        BottomLeft = 8,
        UpFinished = 9,
        DownFinished = 10,
        LeftFinished = 11,
        RightFinished = 12,
    }


    public ArrowDirection TranslateDirection(Tile previousTile, Tile currentTile, Tile futureTile) {
        bool isFinish = futureTile == null;
        bool isStart = previousTile == null;

        Vector2 pastDirection = previousTile != null ? currentTile.gridLocation - previousTile.gridLocation : new Vector2(0, 0);
        Vector2 futureDirection = futureTile != null ? futureTile.gridLocation - currentTile.gridLocation : new Vector2(0, 0);
        Vector2 direction = pastDirection != futureDirection ? pastDirection + futureDirection : futureDirection;


        if (direction == new Vector2(0, 1) && !isFinish) {
            return ArrowDirection.Up;
        }
        if (direction == new Vector2(0, -1) && !isFinish) {
            return ArrowDirection.Down;
        }
        if (direction == new Vector2(1, 0) && !isFinish) {
            return ArrowDirection.Right;
        }
        if (direction == new Vector2(-1, 0) && !isFinish) {
            return ArrowDirection.Left;
        }


        if (direction == new Vector2(1, 1) && !isFinish) {
            if (pastDirection.y < futureDirection.y) {
                return ArrowDirection.BottomLeft;
            } else {
                return ArrowDirection.TopRight;
            }
        }
        if (direction == new Vector2(-1, 1) && !isFinish) {
            if (pastDirection.y < futureDirection.y) {
                return ArrowDirection.BottomRight;
            } else {
                return ArrowDirection.TopLeft;
            }
        }
        if (direction == new Vector2(1, -1) && !isFinish) {
            if (pastDirection.y > futureDirection.y) {
                return ArrowDirection.TopLeft;
            } else {
                return ArrowDirection.BottomRight;
            }
        }
        if (direction == new Vector2(-1, -1) && !isFinish) {
            if (pastDirection.y > futureDirection.y) {
                return ArrowDirection.TopRight;
            } else {
                return ArrowDirection.BottomLeft;
            }
        }

        if (direction == new Vector2(0, 1) && isFinish) {
            return ArrowDirection.UpFinished;
        }
        if (direction == new Vector2(0, -1) && isFinish) {
            return ArrowDirection.DownFinished;
        }
        if (direction == new Vector2(1, 0) && isFinish) {
            return ArrowDirection.RightFinished;
        }
        if (direction == new Vector2(-1, 0) && isFinish) {
            return ArrowDirection.LeftFinished;
        }



        /*if (direction == new Vector2(0, 1) && isStart) {
            return ArrowDirection.UpFirst;
        }
        if (direction == new Vector2(0, -1) && isStart) {
            return ArrowDirection.DownFirst;
        }
        if (direction == new Vector2(1, 0) && isStart) {
            return ArrowDirection.RightFirst;
        }
        if (direction == new Vector2(-1, 0) && isStart) {
            return ArrowDirection.LeftFirst;
        }*/

        return ArrowDirection.None;
    }
}
