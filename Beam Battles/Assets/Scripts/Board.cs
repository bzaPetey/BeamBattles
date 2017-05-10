using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    [SerializeField] Transform pilarPrefab;
    [SerializeField] Transform wallPrefab;
    [SerializeField] Vector2 spacing = new Vector2(2, 2);
    [SerializeField] int mapRows = 10;
    public static float playerOffset = .5f;
    public static int playerMovementStep = 3;


    public void Start()
    {
        //size board
        PrepareBoard();
        //place the pilars
        PlacePilars();
        //place walls
        PlaceOuterWalls();
    }



    void PlaceOuterWalls()
    {
        Transform northWall = Instantiate(wallPrefab);
        Transform eastWall = Instantiate(wallPrefab);
        Transform southWall = Instantiate(wallPrefab);
        Transform westWall = Instantiate(wallPrefab);

        northWall.localScale = new Vector3(mapRows * 3, northWall.transform.localScale.y, northWall.transform.localScale.x);
        northWall.position = new Vector3(mapRows * 1.5f, eastWall.position.y, mapRows * 3);
        northWall.name = "North Wall";

        eastWall.localScale = new Vector3(eastWall.transform.localScale.x, eastWall.transform.localScale.y, mapRows * 3);
        eastWall.position = new Vector3(mapRows * 3, eastWall.position.y, mapRows * 1.5f);
        eastWall.name = "East Wall";

        southWall.localScale = new Vector3(mapRows * 3, southWall.transform.localScale.y, southWall.transform.localScale.x);
        southWall.position = new Vector3(mapRows * 1.5f, southWall.position.y, 0);
        southWall.name = "South Wall";

        westWall.localScale = new Vector3(westWall.transform.localScale.x, westWall.transform.localScale.y, mapRows * 3);
        westWall.position = new Vector3( 0, westWall.position.y, mapRows * 1.5f);
        westWall.name = "West Wall";

    }


    void PrepareBoard()
    {
        float scale = (spacing.x + 1) * mapRows;
        float position = scale * .5f;

        transform.localScale = new Vector3(scale, .1f, scale);
        transform.position = new Vector3(position, 0, position);
    }


    void PlacePilars()
    {
        float posY = pilarPrefab.transform.localScale.y;
        for(int y = 0; y < mapRows + 1; y++)
        {
            for (int x = 0; x < mapRows + 1; x++)
            {
                Transform temp = Instantiate(pilarPrefab, new Vector3(x + x * spacing.x, posY, y + y * spacing.y), Quaternion.identity);
                temp.parent = transform;
            }

        }
    }
}
