using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CappyAI : MonoBehaviour {
  [SerializeField] float speed;
  [SerializeField] float jumpHeight;
  [SerializeField] float attackRange;
  SpriteRenderer sprite;
  Transform playerTrans;
  PlayerMovement playerMove;
  GameObject player;
  int power = 0;


  Transform trans;
  Rigidbody2D body;

  int _health = 10;

  bool hitWall;
  bool inSucc;

  // Start is called before the first frame update
  void Start() {
    player = GameObject.Find("Kirby");
    playerTrans = player.GetComponent<Transform>();
    playerMove = player.GetComponentInChildren<PlayerMovement>();
    trans = GetComponent<Transform>();
    body = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update() {
    if (inSucc) {
      _health -= 10;
      inSucc = false;
    }
    if (_health < 1) {
      playerMove.GiveSucc(power);//give play movement informatiuons before dying

      Die();
    }
  }

  // Update is called regularly
  void FixedUpdate() {
    if (sprite.isVisible) {
      Walk();
    }
  }

  //Destroys entity
  void Die() {
    Destroy(gameObject);
  }

  //Returns AI's health
  public int GetHealth() {
    return _health;
  }

  //AI Movement
  void Walk() {
    if (hitWall) {
      if (trans.rotation[1] == 1f) {
        trans.rotation = Quaternion.Euler(0, 0, 0);
        hitWall = false;
      } else {
        trans.rotation = Quaternion.Euler(0, 180, 0);
        hitWall = false;
      }
    }
    trans.position += transform.right * Time.deltaTime * speed;
  }

  //Acts on collision
  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.collider.tag == "Ground" || collision.collider.tag == "Wall" || collision.collider.tag == "Enemy") {
      for (int i = 0; i < collision.contacts.Length; i++) {
        if (collision.collider.tag == "Wall" || collision.collider.tag == "Enemy") {
          hitWall = true;
          break;
        } else if (collision.contacts[i].normal.x == -1 || collision.contacts[i].normal.x == 1) //Checks if gameObject is colliding with left or right side of object
          {
          hitWall = true;
        }
      }
    }
  }

  //Acts on entry of collider
  void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.tag == "SUCC") {
      inSucc = true;
    } else if (collision.gameObject.tag == "Player Attack") {//dies to player attacks without succ
      Die();
    } else {
      inSucc = false;
    }
  }
}
