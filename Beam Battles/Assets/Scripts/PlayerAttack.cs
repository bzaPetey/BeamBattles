/*
 * PlayerAttack.cs
 * Peter Laliberte - BurgZerg Arcade
 */
using UnityEngine;


[DisallowMultipleComponent]
public class PlayerAttack : MonoBehaviour {
    public static uint MIN_DISTANCE = 1;    //why are the laser distances a float?
    public static uint MAX_DISTANCE = 10;

    [SerializeField] uint curDistance;
    [SerializeField] float shotDur = .5f;
    [SerializeField] LineRenderer[] laser;



    private void Start()
    {
        TurnOffLasers();
    }


    public uint GetLaserDistance
    {
        get { return curDistance;  }
        set
        {
            if (value < MIN_DISTANCE)
                value = MIN_DISTANCE;
            else if (value > MAX_DISTANCE)
                value = MAX_DISTANCE;

            curDistance = value;
        }
    }


    /// <summary>
    /// Fire the players lasers in all 4 directions.
    /// </summary>
    public void Fire()
    {
        for (int cnt = 0; cnt < laser.Length; cnt++)
        {
            laser[cnt].SetPosition(0,transform.position);
            laser[cnt].enabled = true;
        }

        laser[0].SetPosition(1, ShotDistance(Direction.UP));        //up laser
        laser[1].SetPosition(1, ShotDistance(Direction.RIGHT));     //right laser
        laser[2].SetPosition(1, ShotDistance(Direction.DOWN));      //down laser
        laser[3].SetPosition(1, ShotDistance(Direction.LEFT));      //left laser

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


    Vector3 ShotDistance(Direction dir)
    {   
        float distance = (Board.cellSize * curDistance) + Board.playerOffset;

        Vector3 pos = Vector3.zero;
        Vector3 shotDir = Vector3.zero;
        RaycastHit hit;

        switch(dir)
        {
            case Direction.UP:
                shotDir = Vector3.forward;
                break;
            case Direction.RIGHT:
                shotDir = Vector3.right;
                break;
            case Direction.DOWN:
                shotDir = Vector3.back;
                break;
            case Direction.LEFT:
                shotDir = Vector3.left;
                break;
        }

        if (Physics.Raycast(transform.position, shotDir, out hit, distance))
        {
            pos = hit.point;    //do not let the laser go past items
            //we hit something so make it take dmg
        }
        else
            pos = (transform.position + ( shotDir * distance)); //noting in the way, so fire the laser the max distance.

        return pos;
    }
}
