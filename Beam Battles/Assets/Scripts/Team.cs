using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Team {
    [SerializeField] int teamID;
    [SerializeField] int maxPlayers;
    [SerializeField] List<BeamPlayer> players;
    //add a name for the team
    //add a color for the team


    public int TeamID
    {
        get { return teamID; }
        set { teamID = value; }
    }

    public bool IsFull
    {
        get { return players.Count == maxPlayers; }
    }


    public Team()
    {
        players = new List<BeamPlayer>();
        maxPlayers = 1;
    }


    public Team(int playerCount)
    {
        players = new List<BeamPlayer>();
        maxPlayers = playerCount;
    }


    public int NumberOfPlayers
    {
        get { return players.Count; }
        set {
            if (value > 0)
                maxPlayers = value;
        }
    }


    public void AddPlayer(BeamPlayer player)
    {
        if (players.Count < maxPlayers)
            players.Add(player);
    }


    public void RemovePlayer(BeamPlayer player)
    {
        players.Remove(player);
    }
}
