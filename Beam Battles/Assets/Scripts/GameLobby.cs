using Microsoft;
using UnityEngine;
using Xbox.Services.Beam;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class GameLobby : MonoBehaviour {
    [SerializeField] int numberOfTeams = 2;
    [SerializeField] int numberOfPlayerPerTeam = 1;
    [SerializeField] GameManager gm;
    [SerializeField] List<Team> team;
    [SerializeField] GameObject gameLobbyPannel;



    private void Start()
    {
        NumberOfPlayersPerTeam = 4;
        NumberOfTeams = 2;
    }


    private void OnEnable()
    {

        Beam.OnInteractivityStateChanged += OnInteractivityStateChanged;
        Beam.OnBeamButtonEvent += OnButton;
    }


    void OnDisable()
    {
        Beam.OnInteractivityStateChanged -= OnInteractivityStateChanged;
        Beam.OnBeamButtonEvent -= OnButton;
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


    public void StartGame()
    {
        for (int cnt = 0; cnt < NumberOfTeams; cnt++)
            team.Add(new Team(numberOfPlayerPerTeam));

        Beam.GoInteractive();

        gameLobbyPannel.SetActive(false);
    }


    //return a bool if the player was added to the team properly
    public void AssignPlayerToRandomTeam(BeamParticipant player)
    {
        Debug.Log(player.BeamUserName);

        if (team.Count < 1)
            return;

        int rndTeam = Random.Range(0, team.Count);

        //        Debug.Log(rndTeam);

        BeamPlayer bp = new BeamPlayer(player);

        team[rndTeam].AddPlayer(bp);

        if(team[rndTeam].IsFull)
        {
            Debug.Log("Team Full");

            gm.AddTeam(team[rndTeam]);
            team.RemoveAt(rndTeam);
        }
    }





    private void OnButton(object sender, BeamButtonEventArgs e)
    {
        if (Beam.GetButtonUp("joinGame"))
        {
            AssignPlayerToRandomTeam(e.Participant);


            //this does not work yet.
            //need a way to disable the join game button when they press it
            e.Participant.Buttons[0].SetDisabled(true);

//            Beam.Button("joinGame").SetDisabled(true);
        }

    }


    private void OnInteractivityStateChanged(object sender, BeamInteractivityStateChangedEventArgs e)
    {
        if (Beam.InteractivityState == BeamInteractivityState.InteractivityEnabled)
            Debug.Log("Connected");
        else if (Beam.InteractivityState == BeamInteractivityState.InteractivityDisabled)
            Debug.Log("Lost Connection");
    }

}
