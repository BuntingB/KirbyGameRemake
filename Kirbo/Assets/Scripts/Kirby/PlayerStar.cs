using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStar : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.tag == "Enemy") {
      Destroy(gameObject);
    }
    if (collision.tag == "Ground") {
      Destroy(gameObject);
    }
  }
}
