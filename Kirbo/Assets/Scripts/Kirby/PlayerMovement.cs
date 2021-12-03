using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  Transform trans;
  Rigidbody2D body;
  [SerializeField] float speed;
  [SerializeField] float jumpForce;
  [SerializeField] float airJumpForce;
  [SerializeField] Sprite[] kirbySprite;
  [SerializeField] float[] gravity;
  [SerializeField] string powerUpName;
  [SerializeField] GameObject squareHitbox;
  Power power;
  //[SerializeField] PowerUp powerUp;

  bool jumpInput = false;
  bool isGrounded;
  SpriteRenderer sprite;




  // Start is called before the first frame update
  void Start() {
    trans = GetComponent<Transform>();
    body = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
    power = new NoPower(squareHitbox);
    //kirbySprite = Resources.LoadAll<Sprite>("Assets\\Sprites\\Kirby");
  }

  // Update is called once per frame
  void Update() {
    Walk();
    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) {
      Jump();
    }
    if (Input.GetKeyDown(KeyCode.S)) {
      sprite.sprite = kirbySprite[0];
      body.gravityScale = gravity[0];
    }
    if (Input.GetKey(KeyCode.Comma)) {
      Attack();
    } else {
      power.NoAttack();
    }
    
  }
  private void FixedUpdate() {
    if (jumpInput) {
      //Jump();
    }
  }
  void Walk() {
    if (Input.GetKey(KeyCode.D)) {
      trans.rotation = Quaternion.Euler(0, 0, 0);
      trans.position += transform.right * Time.deltaTime * speed;
    } else if (Input.GetKey(KeyCode.A)) {
      trans.rotation = Quaternion.Euler(0, 180, 0);
      trans.position += transform.right * Time.deltaTime * speed;
    }
  }
  void Jump() {
    if (!isGrounded) {
      sprite.sprite = kirbySprite[1];
      body.gravityScale = gravity[1];
      body.velocity = new Vector2(0, airJumpForce);
    } else {
      body.velocity = new Vector2(0,jumpForce);
    }
    //body.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    isGrounded = false;
  }
  private void Attack() {
    if (powerUpName == "Nothing") {
      
    }
    power.Attack();
    // else use powerUp.attack()

  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.collider.tag == "Ground") {
      for (int i = 0; i < collision.contacts.Length; i++) {
        if (collision.contacts[i].normal.y > 0.5) {
          isGrounded = true;
          sprite.sprite = kirbySprite[0];
          body.gravityScale = gravity[0];
        }
      }
    }
  }
  public float GetSpeed() {
    return speed;
  }
}
