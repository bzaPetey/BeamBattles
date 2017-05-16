/*
 * PlayerAttack.cs
 * Peter Laliberte - BurgZerg Arcade
 */
using UnityEngine;


[DisallowMultipleComponent]
public class PlayerAttack : MonoBehaviour {
    [SerializeField] float baseDistance;
    [SerializeField] float curDistance;
    [SerializeField] float shotDur = 1f;
    [SerializeField] LineRenderer[] laser;

    public static float maxDistance = (Board.playerMovementStep) * 10 + Board.playerOffset;


    private void Start()
    {
        Invoke("TurnOffLasers",0);
    }


    /// <summary>
    /// Fire the players lasers in all 4 directions.
    /// </summary>
    public void Fire()
    {
        for (int cnt = 0; cnt < laser.Length; cnt++)
        {
            laser[cnt].SetPosition(0, Vector3.zero);
            laser[cnt].enabled = true;
        }

        laser[0].SetPosition(1, new Vector3(0, 0, curDistance));        //up laser
        laser[1].SetPosition(1, new Vector3(curDistance, 0, 0));        //right laser
        laser[2].SetPosition(1, new Vector3(0, 0, -curDistance));       //down laser
        laser[3].SetPosition(1, new Vector3(-curDistance, 0, 0));       //left laser

        Invoke("TurnOffLasers", shotDur);   //turn the laser off after a set amount of time
    }


    /// <summary>
    /// Turn the lasers off after a set amount of time.
    /// This is called frome and Invoke statment.
    /// </summary>
    void TurnOffLasers()
    {
        for (int cnt = 0; cnt < laser.Length; cnt++)
            laser[cnt].enabled = false;
    }
}
