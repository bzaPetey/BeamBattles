using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour {
    PlayerMovement playerMovement;

    [SerializeField] bool canMoveUp;
    [SerializeField] bool canMoveRight;
    [SerializeField] bool canMoveDown;
    [SerializeField] bool canMoveLeft;



    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }


    private void Start()
    {
        GetAvailableDirections();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && canMoveUp)
            playerMovement.Move(Direction.UP);
        else if (Input.GetKeyDown(KeyCode.D) && canMoveRight)
            playerMovement.Move(Direction.RIGHT);
        else if (Input.GetKeyDown(KeyCode.S) && canMoveDown)
            playerMovement.Move(Direction.DOWN);
        else if (Input.GetKeyDown(KeyCode.A) && canMoveLeft)
            playerMovement.Move(Direction.LEFT);


        GetAvailableDirections();
    }


    public void GetAvailableDirections()
    {
        Ray ray;
        float distance = Board.playerMovementStep + Board.playerOffset + .5f;

        Debug.DrawRay(transform.position, Vector3.left, Color.yellow);
        Debug.DrawRay(transform.position, Vector3.back, Color.yellow);
        Debug.DrawRay(transform.position, Vector3.forward, Color.yellow);
        Debug.DrawRay(transform.position, Vector3.right, Color.yellow);

        RaycastHit hit;

        ray = new Ray(transform.position, Vector3.left);
        if (Physics.Raycast(ray, out hit, distance))    canMoveLeft = false;
        else canMoveLeft = true;

        ray = new Ray(transform.position, Vector3.back);
        if (Physics.Raycast(ray, out hit, distance))    canMoveDown = false;
        else canMoveDown = true;

        ray = new Ray(transform.position, Vector3.forward);
        if (Physics.Raycast(ray, out hit, distance))    canMoveUp = false;
        else canMoveUp = true;

        ray = new Ray(transform.position, Vector3.right);
        if (Physics.Raycast(ray, out hit, distance))    canMoveRight = false;
        else canMoveRight = true;
    }


}
