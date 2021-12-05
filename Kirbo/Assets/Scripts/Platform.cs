using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
  PlatformEffector2D platform;
  BoxCollider2D box;

  // Start is called before the first frame update
  void Start()
  {
    platform = GetComponent<PlatformEffector2D>();
    box = GetComponent<BoxCollider2D>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
      //platform.enabled = false;
      //box.enabled = false;
    } else {
      platform.enabled = true;
      box.enabled = true;
    }
  }
  
  
}
