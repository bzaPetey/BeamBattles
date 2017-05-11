using System.Collections.Generic;
using UnityEngine;

public class Team {
    List<Player> players;
    int maxPlayers;
    //add a name for the team
    //add a color for the team
    

    public Team()
    {
        players = new List<Player>();
        maxPlayers = 1;
    }


    public Team(int playerCount)
    {
        players = new List<Player>();
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


    public void AddPlayer(Player player)
    {
        if (players.Count < maxPlayers)
            players.Add(player);
    }


    public void RemovePlayer(Player player)
    {
        players.Remove(player);
    }
}
