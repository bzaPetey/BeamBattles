using UnityEngine;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class Board : MonoBehaviour {
    [SerializeField] Transform pilarPrefab;
    [SerializeField] Transform wallPrefab;
    [SerializeField] Vector2 spacing = new Vector2(2, 2);
    [SerializeField] int mapRows = 10;
    [Range(0,1)] [SerializeField] float percentOfWallsToMake = .25f;
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
        PlaceInnerWalls();
    }


    void PlaceInnerWalls()
    {
        //inner walls in one direction is (mapRows * 2) - 1 in both directions
        List<int> walls = new List<int>();

        int walllCount = (mapRows * mapRows) * 2 - mapRows * 2;
        int midNumber = ((walllCount) / 2 ) - 1;

        Debug.Log(mapRows + ": " + walllCount + "-" + midNumber);

        for (int cnt = 0; cnt < walllCount; cnt++)
            walls.Add(cnt);

        int totalWalls = walls.Count;

        int numberOfWallsToPlace = Mathf.CeilToInt( walllCount * percentOfWallsToMake);

        for (int dnt = 0; dnt < numberOfWallsToPlace; dnt++)
        {
            int index = Random.Range(0, walls.Count);

            Transform temp = Instantiate(wallPrefab);
            temp.name = "Wall: " + walls[index];

            if (walls[index] <= midNumber)
            {
                //wall number (0-78)/number of rows on the map - 1 (19) = col pos
                int row = walls[index] / (mapRows - 1);
                int col = walls[index] % (mapRows - 1);

                //place the wall running along the z
                temp.position = new Vector3((col + 1) * playerMovementStep, temp.position.y, row * playerMovementStep + 1.5f);
            }
            else
            {
                //wall number (0-78)/number of rows on the map - 1 (19) = col pos
                //                int row = walls[index] / midNumber;
                //                int col = walls[index] % midNumber;

                //20 = [0,0] [col,row]
                //21 = [1,0]
                //22 = [2,0]
                //25 = [0,1]
                //26 = [1,1]
                    
                int row = (walls[index] - (midNumber + 1)) / mapRows;   
                int col = (walls[index] - (midNumber + 1)) % mapRows;

//                Debug.Log((walls[index] - (midNumber + 1)) + "/" + mapRows + "[" + col + "," + row + "]");
                //place the wall running along the x
                temp.position = new Vector3((col * playerMovementStep ) + temp.localScale.z/2, temp.position.y, (row * playerMovementStep) + temp.localScale.z);
                temp.Rotate(Vector3.up, 90);
            }
            temp.parent = transform;
            walls.RemoveAt(index);
        }
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
        northWall.parent = transform;

        eastWall.localScale = new Vector3(eastWall.transform.localScale.x, eastWall.transform.localScale.y, mapRows * 3);
        eastWall.position = new Vector3(mapRows * 3, eastWall.position.y, mapRows * 1.5f);
        eastWall.name = "East Wall";
        eastWall.parent = transform;

        southWall.localScale = new Vector3(mapRows * 3, southWall.transform.localScale.y, southWall.transform.localScale.x);
        southWall.position = new Vector3(mapRows * 1.5f, southWall.position.y, 0);
        southWall.name = "South Wall";
        southWall.parent = transform;

        westWall.localScale = new Vector3(westWall.transform.localScale.x, westWall.transform.localScale.y, mapRows * 3);
        westWall.position = new Vector3( 0, westWall.position.y, mapRows * 1.5f);
        westWall.name = "West Wall";
        westWall.parent = transform;
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
