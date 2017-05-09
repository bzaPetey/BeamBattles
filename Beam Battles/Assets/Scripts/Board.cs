using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    [SerializeField] Transform pilarPrefab;
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
        for(int y = 0; y < mapRows; y++)
        {
            for (int x = 0; x < mapRows; x++)
            {
                Transform temp = Instantiate(pilarPrefab, new Vector3(x + x * spacing.x, posY, y + y * spacing.y), Quaternion.identity);
                temp.parent = transform;
            }

        }
    }
}
