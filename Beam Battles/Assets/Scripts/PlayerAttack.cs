using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAttack : MonoBehaviour {
    [SerializeField] float baseDistance;
    [SerializeField] float curDistance;
    [SerializeField] float shotDur = 1f;

    [SerializeField] LineRenderer[] laser;

    public static float maxDistance = (Board.playerMovementStep ) * 10 + Board.playerOffset;


    private void Start()
    {
        Invoke("TurnOffLasers",0);
    }


    public void Fire()
    {
        for (int cnt = 0; cnt < laser.Length; cnt++)
        {
            laser[cnt].SetPosition(0, Vector3.zero);
            laser[cnt].enabled = true;
        }

        Vector3 up = new Vector3(0, 0, curDistance);
        Vector3 right = new Vector3(curDistance, 0, 0);
        Vector3 down = new Vector3(0, 0,-curDistance);
        Vector3 left = new Vector3(-curDistance, 0, 0);

        laser[0].SetPosition(1, up);
        laser[1].SetPosition(1, right);
        laser[2].SetPosition(1, down);
        laser[3].SetPosition(1, left);

        Invoke("TurnOffLasers", shotDur);
    }


    void TurnOffLasers()
    {
        for (int cnt = 0; cnt < laser.Length; cnt++)
            laser[cnt].enabled = false;
    }
}
