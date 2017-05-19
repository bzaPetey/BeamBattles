﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[DisallowMultipleComponent]
public class Board : MonoBehaviour {
    [SerializeField] Transform pilarPrefab;
    [SerializeField] Transform wallPrefab;
    [SerializeField] Vector2 spacing = new Vector2(2, 2);
    public static int mapRows = 10;
    [Range(0,1)] [SerializeField] float percentOfWallsToMake = .25f;
    public static float playerOffset = .5f;
    public static int cellSize = 3;


    public void Start()
    {
        PlaceBoard();                       //set the size of the board
        PlaceOuterWalls();                  //place the outside walls and make sure the texture is tiled right
        PlaceInnerWalls();
        PlacePilars();
    }


    /// <summary>
    /// Place a set percentage of walls on the board.
    /// </summary>
    void PlaceInnerWalls()
    {
        List<int> walls = new List<int>();

        int walllCount = (mapRows * mapRows) * 2 - mapRows * 2;
        int midNumber = ((walllCount) / 2 ) - 1;

        for (int cnt = 0; cnt < walllCount; cnt++)
            walls.Add(cnt);

        int totalWalls = walls.Count;

        int numberOfWallsToPlace = Mathf.CeilToInt( walllCount * percentOfWallsToMake);

        for (int dnt = 0; dnt < numberOfWallsToPlace; dnt++)
        {
            int index = Random.Range(0, walls.Count);

            Transform temp = Instantiate(wallPrefab);
            temp.name = string.Format("Wall: {0}", walls[index]);

            if (walls[index] <= midNumber)
            {
                int row = walls[index] / (mapRows - 1);
                int col = walls[index] % (mapRows - 1);

                temp.position = new Vector3((col + 1) * cellSize, temp.position.y, row * cellSize + 1.5f);
            }
            else
            {
                int row = (walls[index] - (midNumber + 1)) / mapRows;
                int col = (walls[index] - (midNumber + 1)) % mapRows;

                temp.position = new Vector3((col * cellSize) + temp.localScale.z / 2, temp.position.y, (row * cellSize) + temp.localScale.z);
                temp.Rotate(Vector3.up, 90);
            }

            temp.parent = transform;
            walls.RemoveAt(index);
        }

        Debug.Log("Done placing walls");
    }


    /// <summary>
    /// Place the outside walls and tile the wall texture properly.
    /// </summary>
    void PlaceOuterWalls()
    {
        //create the 4 walls
        Transform northWall = Instantiate(wallPrefab);
        Transform eastWall = Instantiate(wallPrefab);
        Transform southWall = Instantiate(wallPrefab);
        Transform westWall = Instantiate(wallPrefab);

        int offset = mapRows * 3;

        //adjust the north wall
        northWall.localScale = new Vector3(offset, northWall.transform.localScale.y, northWall.transform.localScale.x);
        northWall.position = new Vector3(mapRows * 1.5f, eastWall.position.y, mapRows * 3);
        northWall.GetComponent<Renderer>().material.mainTextureScale = new Vector2(offset, 4);
        northWall.name = "North Wall";
        northWall.parent = transform;

        //adjust the east wall
        eastWall.localScale = new Vector3(eastWall.transform.localScale.x, eastWall.transform.localScale.y, offset);
        eastWall.position = new Vector3(mapRows * 3, eastWall.position.y, mapRows * 1.5f);
        eastWall.GetComponent<Renderer>().material.mainTextureScale = new Vector2(offset, 4);
        eastWall.name = "East Wall";
        eastWall.parent = transform;

        //adjust the south wall
        southWall.localScale = new Vector3(offset, southWall.transform.localScale.y, southWall.transform.localScale.x);
        southWall.position = new Vector3(mapRows * 1.5f, southWall.position.y, 0);
        southWall.GetComponent<Renderer>().material.mainTextureScale = new Vector2(offset, 4);
        southWall.name = "South Wall";
        southWall.parent = transform;

        //adjust the west wall
        westWall.localScale = new Vector3(westWall.transform.localScale.x, westWall.transform.localScale.y, offset);
        westWall.position = new Vector3( 0, westWall.position.y, mapRows * 1.5f);
        westWall.GetComponent<Renderer>().material.mainTextureScale = new Vector2(offset, 4);
        westWall.name = "West Wall";
        westWall.parent = transform;
    }


    /// <summary>
    /// Position and scale the board in the scene
    /// </summary>
    void PlaceBoard()
    {
        float scale = (spacing.x + 1) * mapRows;
        float position = scale * .5f;

        transform.localScale = new Vector3(scale, .1f, scale);
        transform.position = new Vector3(position, 0, position);

        GetComponent<Renderer>().material.mainTextureScale = new Vector2(mapRows, mapRows);

    }


    /// <summary>
    /// Place the pilars at a set interval
    /// </summary>
    void PlacePilars()
    {
        float posY = pilarPrefab.transform.localScale.y;

        for (int y = 0; y < mapRows + 1; y++)
        {
            for (int x = 0; x < mapRows + 1; x++)
            {
                Transform temp = Instantiate(pilarPrefab, new Vector3(x + x * spacing.x, posY, y + y * spacing.y), Quaternion.identity);
                temp.parent = transform;
            }
        }
        Debug.Log("Done placing pillars");
    }
}
