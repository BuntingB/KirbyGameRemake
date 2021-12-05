using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : Power
{
  [SerializeField] GameObject cutterPrefab;
  GameObject player;
  PlayerMovement playerMove;
  float attackInterval;
  private void Start() {
    player = GameObject.Find("Kirby");
    playerMove = player.GetComponent<PlayerMovement>();
    attackInterval = 0;
  }
  public override void Attack() {
    if (GameObject.Find("Cutter Attack(Clone)") == null) {
      var star = Instantiate(cutterPrefab, player.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
      star.GetComponent<Rigidbody2D>().velocity = new Vector3(playerMove.PlayerGetRight(), 0, 0) * 20;
      Destroy(star, 1f);
      attackInterval = Time.realtimeSinceStartup + 2f;
    }
  }
  public override void NoAttack() {
    
  }
}
