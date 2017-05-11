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
                pos += Vector3.forward * Board.playerMovementStep;
                break;
            case Direction.RIGHT:
                pos += Vector3.right * Board.playerMovementStep;
                break;
            case Direction.DOWN:
                pos += Vector3.back * Board.playerMovementStep;
                break;
            case Direction.LEFT:
                pos += Vector3.left * Board.playerMovementStep;
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
