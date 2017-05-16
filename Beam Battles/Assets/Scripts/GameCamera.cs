/*
 * GameCamera.cs
 * Peter Laliberte - BurgZerg Arcade
 */
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
public class GameCamera : MonoBehaviour {
    [SerializeField] float distanceToPlayer = 20f;
    Transform target;


    /// <summary>
    /// return the vertical distance from the camera to the target player.
    /// </summary>
    public float VerticalDistance
    {
        get { return distanceToPlayer; }
    }


    /// <summary>
    /// Move to the target player.
    /// </summary>
    /// <param name="target"></param>
    public void MoveToPlayer(Transform target)
    {
        this.target = target;
    }
}
