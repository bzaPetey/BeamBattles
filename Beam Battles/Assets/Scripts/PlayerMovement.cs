using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour {
    public void Move(Direction dir)
    {
        Vector3 pos = transform.position;

        switch(dir)
        {
            case Direction.UP:
                pos += Vector3.forward * Board.cellSize;
                break;
            case Direction.RIGHT:
                pos += Vector3.right * Board.cellSize;
                break;
            case Direction.DOWN:
                pos += Vector3.back * Board.cellSize;
                break;
            case Direction.LEFT:
                pos += Vector3.left * Board.cellSize;
                break;
        }

        transform.position = pos;
    }

}

public enum Direction
{
    UP,
    RIGHT,
    DOWN,
    LEFT
}
