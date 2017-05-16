using Microsoft;
using UnityEngine;
using Xbox.Services.Beam;
using System.Collections.Generic;


[DisallowMultipleComponent]
public class BeamInput : MonoBehaviour {

    private void Start()
    {
        Beam.GoInteractive();
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



    void GetViewerStats(BeamParticipant viewer)
    {
        BeamPlayer bp = new BeamPlayer(viewer);
        bp.ToString();
//      Debug.Log(viewer.BeamUserName + " is level " + viewer.BeamLevel);
    }


    private void OnButton(object sender, BeamButtonEventArgs e)
    {
        if(Beam.GetButtonUp("joinGame"))
        {
            GetViewerStats(e.Participant);

        }

    }


    private void OnInteractivityStateChanged(object sender, BeamInteractivityStateChangedEventArgs e)
    {
        if (Beam.InteractivityState == BeamInteractivityState.InteractivityEnabled)
        {
            Debug.Log("Connected");
        }
    }
}
