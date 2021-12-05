using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {
  [SerializeField] Transform connectedDoorTrans;
  [SerializeField] Doors connectedDoor;
  [SerializeField] Transform playerTrans;
  bool canEnter = false;

  float timeToTeleport;
  float teleportDelay = 1.5f;

  bool coolDown;


  void Start() {

  }


  void Update() {
    if (Input.GetKeyDown(KeyCode.E) && canEnter) {
      playerTrans.position = connectedDoorTrans.position;
    }
  }


  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.tag == "Player") {
      canEnter = true;
    }
  }
  private void OnTriggerExit2D(Collider2D collision) {
    if (collision.gameObject.tag == "Player") {
      canEnter = false;
    }
  }

}
