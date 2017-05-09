﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            Move(Direction.UP);
        else if (Input.GetKeyDown(KeyCode.D))
            Move(Direction.RIGHT);
        else if (Input.GetKeyDown(KeyCode.S))
            Move(Direction.DOWN);
        else if (Input.GetKeyDown(KeyCode.A))
            Move(Direction.LEFT);
    }


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
