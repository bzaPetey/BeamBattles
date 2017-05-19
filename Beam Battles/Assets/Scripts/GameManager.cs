using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour {
    [SerializeField] Board board;
    [SerializeField] List<Team> team;

    [SerializeField] Player player;


    public void GameSetup()
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


    public void AddTeam(Team t)
    {
        team.Add(t);
    }
}

public enum GameState {
    LOBBY,      //waiting for the players to join the game
    SETUP,      //we have the players, but are setting up the game. ie) game board, positioning players...
    PLAYING,    //the game has started and we are taking turns playing
    ENDING,     //the game has ended and we are displaying stats
    RESET       //clean up the scene so that we can start a new game
}
