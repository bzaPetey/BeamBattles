using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour {
    [SerializeField] GameObject playerPrefab;
}
