using Microsoft;
using System;
using UnityEngine;
using UnityEngine.UI;
using Xbox.Services.Beam;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class GameLobby : MonoBehaviour {
    public static int minTeamCount = 2;
    public static int minPlayersPerTeam = 1;

    [SerializeField] int numberOfTeams = 2;
    [SerializeField] int numberOfPlayerPerTeam = 1;
    [SerializeField] GameManager gm;
    [SerializeField] List<Team> team;
    [SerializeField] List<BeamParticipant> beamPlayers;
    [SerializeField] GameObject gameLobbyPannel;
    [SerializeField] BeamGroup crowdGroup;
    [SerializeField] BeamGroup playerGroup;
    [SerializeField] Text teamCountDisplay;
    [SerializeField] Text playerCountPerTeamDisplay;



    private void Start()
    {
        numberOfTeams = minTeamCount;
        numberOfPlayerPerTeam = minPlayersPerTeam;

        teamCountDisplay.text = numberOfTeams.ToString();
        playerCountPerTeamDisplay.text = numberOfPlayerPerTeam.ToString();
    }

    private void OnEnable()
    {
        Beam.OnInteractivityStateChanged += OnInteractivityStateChanged;
        Beam.OnParticipantStateChanged += OnParticipantStateChanged;
        Beam.OnBeamButtonEvent += OnButton;
        Beam.OnGoInteractive += OnGoInteractive;
    }


    void OnDisable()
    {
        Beam.OnInteractivityStateChanged -= OnInteractivityStateChanged;
        Beam.OnParticipantStateChanged -= OnParticipantStateChanged;
        Beam.OnBeamButtonEvent -= OnButton;
        Beam.OnGoInteractive -= OnGoInteractive;
    }


    public void NumberOfTeams(float num)
    {
        numberOfTeams = (int)num;
        teamCountDisplay.text = numberOfTeams.ToString();
    }


    public void NumberOfPlayersPerTeam(float num)
    {
        numberOfPlayerPerTeam = (int)num;
        playerCountPerTeamDisplay.text = numberOfPlayerPerTeam.ToString();
    }


    public void StartGame()
    {
        for (int cnt = 0; cnt < numberOfTeams; cnt++)
            team.Add(new Team(numberOfPlayerPerTeam));

        Beam.GoInteractive();

        gameLobbyPannel.SetActive(false);
    }


    public void AssignPlayerToRandomTeam()
    {
        if (team.Count < 1)
            return;

        Debug.Log("Asigning Teams");

        foreach(BeamParticipant p in beamPlayers)
        {
            int rndTeam = UnityEngine.Random.Range(0, team.Count);

            BeamPlayer bp = new BeamPlayer(p);

            team[rndTeam].AddPlayer(bp);

            if (team[rndTeam].IsFull)
            {
                gm.AddTeam(team[rndTeam]);
                team.RemoveAt(rndTeam);
            }
        }
    }



    void AddPlayer(BeamParticipant bp)
    {
//      Debug.Log("Contains " + bp.BeamUserName+ ": " +  beamParticipants.Contains(bp));
        if (beamPlayers.Contains(bp))
            return;

        beamPlayers.Add(bp);

//      Debug.Log("Added: " + bp.BeamUserName + ". Now we have a total of " + beamPlayers.Count);

        if (beamPlayers.Count < numberOfPlayerPerTeam * numberOfTeams)
            return;

        CreateTeams();
    }


    void CreateTeams()
    {
        Debug.Log("Creating Teams");
        Debug.Log("Number of viewers: " + BeamManager.SingletonInstance.Participants.Count);

        for (int cnt = 0; cnt < BeamManager.SingletonInstance.Participants.Count; cnt++)
        {
            if (beamPlayers.Contains(Beam.Participants[cnt]))
            {
                Debug.Log("Assigning: " + Beam.Participants[cnt].BeamUserName + " to group: Player Group");
                Beam.Participants[cnt].Group = playerGroup;
            }
            else
            {
                Debug.Log("Assigning: " + Beam.Participants[cnt].BeamUserName + " to group: Crowd Group");
                Beam.Participants[cnt].Group = crowdGroup;
            }
        }

        AssignPlayerToRandomTeam();
    }


    void Viewers()
    {
        Debug.Log("viewers in room: " + BeamManager.SingletonInstance.Participants.Count);
        foreach (BeamParticipant p in BeamManager.SingletonInstance.Participants)
            Debug.LogWarning(p.BeamUserName);
    }


    private void OnButton(object sender, BeamButtonEventArgs e)
    {
        //if the button pressed was the joinGame button
        //put the button on cooldown
        //add the player to the list of people that are going to be split in to teams

        if (Beam.GetButtonUp("joinGame"))
        {
//            e.Participant.Buttons[0].TriggerCooldown(3000);
//          BeamManager.SingletonInstance.TriggerCooldown("joinGame", 3000);

            AddPlayer(e.Participant);

//            AssignPlayerToRandomTeam(e.Participant);

            //this does not work yet.
            //need a way to disable the join game button when they press it
//            e.Participant.Buttons[0].SetDisabled(true);

        }

    }


    private void OnInteractivityStateChanged(object sender, BeamInteractivityStateChangedEventArgs e)
    {
        if (Beam.InteractivityState == BeamInteractivityState.InteractivityEnabled)
            Debug.Log("Connected");
        else if (Beam.InteractivityState == BeamInteractivityState.InteractivityDisabled)
            Debug.Log("Lost Connection");
    }


    void OnParticipantStateChanged(object sender, BeamParticipantStateChangedEventArgs e)
    {
        //Debug.Log(e.Participant.BeamUserName + ": " + e.Participant.State);
 //       Viewers();
    }


    void OnGoInteractive(object sender, BeamEventArgs e)
    {
        crowdGroup = new BeamGroup("crowd", "crowd");
        playerGroup = new BeamGroup("players", "gameLobby");

//        Viewers();
    }
}
