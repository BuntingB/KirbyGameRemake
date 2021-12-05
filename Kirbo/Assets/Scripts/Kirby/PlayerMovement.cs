using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
  Transform trans;
  Rigidbody2D body;
  [SerializeField] float speed;
  [SerializeField] float jumpForce;
  [SerializeField] float airJumpForce;
  [SerializeField] Sprite[] kirbySprite;
  [SerializeField] float[] gravity;
  [SerializeField] string powerUpName;
  [SerializeField] GameObject squareHitbox;
  [SerializeField] Power[] powers;
  [SerializeField] GameObject starPrefab;
  int currentPower = 0;
  public int inMouth = -1;
  public Animator anim;
  int right = 1;
  [SerializeField] Image cutterIcon;


  Power power;
  //[SerializeField] PowerUp powerUp;

  bool jumpInput = false;
  bool isGrounded;
  bool isPuffed;
  SpriteRenderer sprite;




  // Start is called before the first frame update
  void Start() {
    cutterIcon.enabled = false;
    trans = GetComponent<Transform>();
    body = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();

    //power = GetComponentInParent<NoPower>();
   //NoPower(squareHitbox);
    //kirbySprite = Resources.LoadAll<Sprite>("Assets\\Sprites\\Kirby");
  }

  // Update is called once per frame
  void Update() {//update need to control the movement horizontal, vertical and attacks
    //attacks
    if (inMouth >= 0) {
      anim.SetBool("Full", true);

    } else {
      anim.SetBool("Full", false);

    }
    if (Input.GetKey(KeyCode.Comma) && inMouth < 0) {
      if (isPuffed) {//depuffs
        //sprite.sprite = kirbySprite[0];
        Puff();
        body.gravityScale = gravity[0];
      } else {
        powers[currentPower].Attack();
      }
    } else {//can only move when not attacking
      Walk();  //horizontal
      powers[currentPower].NoAttack();
    }
    if (inMouth >= 0 && Input.GetKeyDown(KeyCode.Comma)) {
      Spit();
    }
    //vertical
    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {//jump
      Jump();
    }
    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
      if (isPuffed) {//depuffs
        //sprite.sprite = kirbySprite[0];
        Puff();
        
        body.gravityScale = gravity[0];
      } else if (inMouth >= 0) {//swallows if 
        Swallow();
      }
    }
  }
  void Walk() {
    if (Input.GetKey(KeyCode.D)) {
      anim.SetBool("Walk", true);
      trans.rotation = Quaternion.Euler(0, 0, 0);
      trans.position += transform.right * Time.deltaTime * speed;
      right = 1;
    } else if (Input.GetKey(KeyCode.A)) {
      anim.SetBool("Walk", true);
      trans.rotation = Quaternion.Euler(0, 180, 0);
      trans.position += transform.right * Time.deltaTime * speed;
      right = -1;
    } else {
      anim.SetBool("Walk", false);
    }
  }
  void Jump() {
    if (!isGrounded && inMouth < 0) {
      //sprite.sprite = kirbySprite[1];
      body.gravityScale = gravity[1];
      body.velocity = new Vector2(0, airJumpForce);
      isPuffed = true;
      anim.SetBool("Float", true);

    } else if (isGrounded) {
      body.velocity = new Vector2(0,jumpForce);
    }
    isGrounded = false;
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.collider.tag == "Ground") {
      for (int i = 0; i < collision.contacts.Length; i++) {
        if (collision.contacts[i].normal.y > 0.5) {
          isGrounded = true;
          if (isPuffed) {
            Puff();
          }
          sprite.sprite = kirbySprite[0];
          body.gravityScale = gravity[0];
        }
      }
    }
  }
  public float GetSpeed() {
    return speed;
  }
  void Swallow() {
    currentPower = inMouth;
    if (currentPower > 0) {
      cutterIcon.enabled = true;
    }
    inMouth = -1;
  }
  void Puff() {
    anim.SetBool("Float", false);
    var star = Instantiate(starPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
    star.GetComponent<Rigidbody2D>().velocity = new Vector3(right, 0, 0) * 20;
    Destroy(star, 0.25f);
    isPuffed = false;
  }
  void Spit() {
    var star = Instantiate(starPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
    star.GetComponent<Rigidbody2D>().velocity = new Vector3(right, 0, 0) * 20;
    anim.SetBool("Full", false);
    inMouth = -1;
  }
  public int PlayerGetRight() {
    return right;
  }
  public void GiveSucc(int power) {
    inMouth = power;
  }
}
