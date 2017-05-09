using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GameCamera : MonoBehaviour {
    [SerializeField] float distanceToPlayer = 20f;
    Transform target;

//    private void LateUpdate()
//    {
//        //for the player
//    }


    public float VerticalDistance
    {
        get { return distanceToPlayer; }
    }


    public void MoveToPlayer(Transform target)
    {
        this.target = target;
    }
}
