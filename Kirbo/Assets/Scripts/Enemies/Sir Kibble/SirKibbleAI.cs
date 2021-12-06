using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirKibbleAI : MonoBehaviour {
  [SerializeField] float jumpHeight;
  [SerializeField] float attackRange;
  [SerializeField] GameObject boomerangPrefab;
  [SerializeField] Transform boomerangSpawn;
  [SerializeField] float boomerangSpeed;
  SpriteRenderer sprite;


  public Transform playerTrans;
  public PlayerMovement playerMove;
  GameObject player;
  int power = 1;

  Transform trans;
  Rigidbody2D body;

  int _health = 10;

  bool isGrounded;
  bool inSucc;
  bool inCoolDown;
  float boomerangCooldown;

  // Start is called before the first frame update
  void Start() {
    player = GameObject.Find("Kirby");
    playerTrans = player.GetComponent<Transform>();
    playerMove = player.GetComponent<PlayerMovement>();
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
    Attack();
  }

  //Destroys entity
  void Die() {
    Destroy(gameObject);
  }

  //Runs Kibble's attack cycle
  void Attack() {
    if (sprite.isVisible) {
      Turn();
      if (!inCoolDown) {
        Jump();

        //Create and throw boomerang
        var boomerang = Instantiate(boomerangPrefab, boomerangSpawn.position, Quaternion.Euler(new Vector3(0, 0, 90)));
        boomerang.GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(playerTrans.position - trans.position) * boomerangSpeed;

        Destroy(boomerang, 5);

        boomerangCooldown = Time.realtimeSinceStartup + 2f;
        inCoolDown = true;
      } else if (Time.realtimeSinceStartup > boomerangCooldown) {
        inCoolDown = false;
      }
    }
  }

  //Makes entity jump
  void Jump() {
    if (isGrounded) {
      body.velocity = new Vector2(0, jumpHeight);
      isGrounded = false;
    }
  }

  //Turns AI to face player
  void Turn() {
    if (playerTrans.position.x > trans.position.x) {
      trans.rotation = Quaternion.Euler(0, 0, 0);
    } else {
      trans.rotation = Quaternion.Euler(0, 180, 0);
    }
  }

  //Returns AI's health
  public int GetHealth() {
    return _health;
  }

  //Acts on collision
  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.collider.tag == "Ground") {
      for (int i = 0; i < collision.contacts.Length; i++) {
        if (collision.contacts[i].normal.y > 0.5) //
        {
          isGrounded = true;
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
