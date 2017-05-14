using UnityEngine;

[System.Serializable]
public class BeamPlayer {
    [SerializeField] string beamName;
    [SerializeField] string beamID;
    public bool isDead = false;



    public BeamPlayer()
    {
        beamName = "Random Awesome Viewer";
        beamID = Random.Range(0, 1000000).ToString();
    }


    public string Name
    {
        get { return beamName; }
        set { beamName = value; }
    }


    public string BeamID
    {
        get { return BeamID; }
        set { BeamID = value; }
    }
}
