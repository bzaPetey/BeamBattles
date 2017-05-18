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
    [SerializeField] List<BeamParticipant> beamParticipants = new List<BeamParticipant>();
    [SerializeField] GameObject gameLobbyPannel;
    [SerializeField] BeamGroup crowd;// = new BeamGroup("crowd", "crowd");
    [SerializeField] BeamGroup players;// = new BeamGroup("players", "gameLobby");



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
    public void AssignPlayerToRandomTeam()
    {
        if (team.Count < 1)
            return;

        foreach(BeamParticipant p in beamParticipants)
        {
            int rndTeam = Random.Range(0, team.Count);
            Debug.Log(rndTeam);

            BeamPlayer bp = new BeamPlayer(p);

            team[rndTeam].AddPlayer(bp);

            if (team[rndTeam].IsFull)
            {
                Debug.Log("Team Full");

                gm.AddTeam(team[rndTeam]);
                team.RemoveAt(rndTeam);
            }

            Debug.Log(bp.ToString());
        }
    }



    void AddPlayer(BeamParticipant bp)
    {
        //check to see if they are already in the list, and add them if not

        //check to see if we have enough players to start the game, if so change (beam)scenes so people can not join the game
        //        BeamPlayer bp = new BeamPlayer(bpart);

//      Debug.Log("Contains " + bp.BeamUserName+ ": " +  beamParticipants.Contains(bp));
        if (beamParticipants.Contains(bp))
            return;

        beamParticipants.Add(bp);
//        Debug.Log("Added: " + bp.BeamUserName + ". Now we have a total of " + beamParticipants.Count);

        if (beamParticipants.Count < numberOfPlayerPerTeam * numberOfTeams)
            return;

        CreateTeams();
    }


    void CreateTeams()
    {
        foreach (BeamParticipant p in BeamManager.SingletonInstance.Participants)
            p.Group = crowd;

        foreach (BeamParticipant p in beamParticipants)
            p.Group = players;

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
            //            BeamManager.SingletonInstance.TriggerCooldown("joinGame", 3000);

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
//        Debug.Log(e.Participant.BeamUserName + ": " + e.Participant.State);
        //when people leave the channel, they are not removed from the participants list
        //so we are going to do it manually.
        if(e.Participant.State == BeamParticipantState.Left)
            BeamManager.SingletonInstance.Participants.Remove(e.Participant);

        //Debug.Log("Viewers: " + BeamManager.SingletonInstance.Participants.Count);

        //foreach (BeamParticipant p in Beam.Participants)
        //    Debug.Log(p.BeamUserName);
//        Viewers();
    }

    void OnGoInteractive(object sender, BeamEventArgs e)
    {
        crowd = new BeamGroup("crowd", "crowd");
        players = new BeamGroup("players", "gameLobby");

//        Viewers();
    }
}
