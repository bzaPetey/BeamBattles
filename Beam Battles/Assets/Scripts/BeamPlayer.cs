/*
 * BeamPlayer.cs
 * Peter Laliberte - BurgZerg Arcade
 */
using UnityEngine;
using Xbox.Services.Beam;

[System.Serializable]
public class BeamPlayer {
    [SerializeField] string beamName;
    [SerializeField] uint beamID;
    [SerializeField] uint beamLevel;
    [SerializeField] bool isDead = false;

    //grab some of the more interesting states that we can about the viewer from beam, level, avatar (if any)...



    public BeamPlayer(BeamParticipant bp)
    {
        beamName = bp.BeamUserName;
        beamID = bp.BeamID;
        beamLevel = bp.BeamLevel;
        isDead = false;
    }


    public string Name
    {
        get { return beamName; }
        set { beamName = value; }
    }


    public uint BeamID
    {
        get { return beamID; }
        set { beamID = value; }
    }


    public uint BeamLevel
    {
        get { return beamLevel; }
        set { beamLevel = value; }
    }


    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }


     public override string ToString()
    {
        return string.Format("{0} is level {1}, and is {2}",beamName, beamLevel, isDead);
    }
}
