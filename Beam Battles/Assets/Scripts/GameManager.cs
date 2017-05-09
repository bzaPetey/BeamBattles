using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] Board board;
    [SerializeField] Player player;


    private void Start()
    {
        GameSetup();
    }


    void GameSetup()
    {
        //create the board
        Instantiate(board);

        //place the players
        Player temp =  Instantiate(player, new Vector3(Board.playerOffset + 1, player.transform.localScale.y * .5f, Board.playerOffset + 1), Quaternion.identity);

        //place the camera on player 1
        Camera.main.transform.position = new Vector3( temp.transform.position.x, Camera.main.GetComponent<GameCamera>().VerticalDistance, temp.transform.position.x);
        Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);
        Camera.main.transform.parent = temp.transform;
    }
}
