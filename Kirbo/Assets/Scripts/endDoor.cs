using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endDoor : MonoBehaviour
{
  [SerializeField] Transform playerTrans;
  [SerializeField] Image endscreen;
  bool canEnter = false;
  // Start is called before the first frame update
  void Start()
  {
    endscreen.enabled = false;

  }

  // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown(KeyCode.E) && canEnter) {
      endscreen.enabled = true;
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
