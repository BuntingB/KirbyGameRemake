using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] Transform _connectedDoorTrans;
    [SerializeField] Doors _connectedDoor;
    [SerializeField] Transform playerTrans;

    float timeToTeleport;
    float teleportDelay = 1.5f;

    bool coolDown;

    //
    void Start()
    {
        
    }

    //
    void Update()
    {
        if (coolDown)
        {
            if (timeToTeleport < Time.realtimeSinceStartup) {
                timeToTeleport = Time.realtimeSinceStartup + teleportDelay;
                coolDown = false;
            }
        }
    }

    //Sends a delay on player teleportation to avoid teleporting back
    void SendDelay()
    {
        _connectedDoor.PlayerSendDelay();
    }

    //Receives the sent delay
    public void PlayerSendDelay() 
    {
        coolDown = true;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SendDelay();
            if (!coolDown && Input.GetKey(KeyCode.E))
            {
                playerTrans.position = _connectedDoorTrans.position;
                coolDown = true;
                Debug.Log("This should only fire once");
            }
        }
    }
}
