using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameLobby : MonoBehaviour {
    [SerializeField] int numberOfTeams = 2;
    [SerializeField] int numberOfPlayerPerTeam = 1;
    [SerializeField] GameManager gm;
    [SerializeField] List<Team> team;



    private void Start()
    {
        NumberOfPlayersPerTeam = 4;
        NumberOfTeams = 3;

        for(int cnt = 0; cnt < numberOfTeams; cnt++)
            team.Add(new Team(NumberOfPlayersPerTeam));
    }


    public int NumberOfTeams
    {
        get { return numberOfTeams; }
        set
        {
            if (value < 2)
                value = 2;

            numberOfTeams = value;
        }
    }


    public int NumberOfPlayersPerTeam
    {
        get { return numberOfPlayerPerTeam; }
        set
        {
            if (value < 1)
                value = 1;

            numberOfPlayerPerTeam = value;
        }
    }


    //create a gui that allows the streamer to change these values before the viewers are allowed to join the game.

    //if a team is stil in the list, then that means that there is an empty spot in it for a player.
    //add a player to a random team and then check to see if that team is full
    //if that team is full, pass it back to the GameManager and remove it from this list
    public void AssignPlayerToRandomTeam(BeamPlayer player)
    {
        if (team.Count < 1)
            return;

        int rndTeam = Random.Range(0, team.Count);

        Debug.Log(rndTeam);

        team[rndTeam].AddPlayer(player);

        if(team[rndTeam].IsFull)
        {
            Debug.Log("Team Full");

            gm.AddTeam(team[rndTeam]);
            team.RemoveAt(rndTeam);
        }
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            AssignPlayerToRandomTeam(new BeamPlayer());
        }
    }
}
