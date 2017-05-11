using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameLobby : MonoBehaviour {
    [SerializeField] int numberOfTeams = 2;
    [SerializeField] int numberOfPlayerPerTeam = 1;

    //create a gui that allows the streamer to change these values before the viewers are allowed to join the game.

    public void AssignPlayerToRandomTeam(Player player)
    {
        //get the teams that are not full yet
        //assign the new player to one of those teams at random

    }
}
