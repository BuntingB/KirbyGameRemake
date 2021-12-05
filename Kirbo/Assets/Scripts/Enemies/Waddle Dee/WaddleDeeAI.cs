using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaddleDeeAI: MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    [SerializeField] float attackRange;
    [SerializeField] Sprite[] aiSprite;
  
  Transform playerTrans;
  PlayerMovement playerMove;
  GameObject player;
  int power = 0;


    Transform trans;
    Rigidbody2D body;
    SpriteRenderer sprite;

    int _health = 10;

    bool hitWall;
    bool isGrounded;
    bool inSucc;
    
    // Start is called before the first frame update
    void Start()
    {
    player = GameObject.Find("Player");
    playerTrans = player.GetComponent<Transform>();
    playerMove = player.GetComponentInChildren<PlayerMovement>();
        trans = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
    void FixedUpdate()
    {
        if (Mathf.Abs(playerTrans.position.x - trans.position.x) < attackRange) {
            Walk();
        }
    }

    //Destroys entity
    void Die() {
        
        Destroy(gameObject);
    }

    //Returns AI's health
    public int GetHealth()
    {
        return _health;
    }

    //AI Movement
    void Walk()
    {
        if (hitWall) {
            if (trans.rotation[1] == -1) {
                trans.rotation = Quaternion.Euler(0, 0, 0);
                hitWall = false;
            }
            else {
                trans.rotation = Quaternion.Euler(0, 180, 0);
                hitWall = false;
            }
        }
        trans.position -= transform.right * Time.deltaTime * speed;
    }

    //Acts on collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                if (collision.contacts[i].normal.y > 0.5) //
                {
                    isGrounded = true;
                }
            }
        }
        if (collision.collider.tag == "Wall" /*|| collision.collider.tag == "Ground"*/) 
        {
            /*for (int i = 0; i < collision.contacts.Length; i++) 
            {
                if (collision.contacts[i].normal.x == -1 || collision.contacts[i].normal.x == 1) //Checks if gameObject is colliding with left or right side of object
                {*/
                    hitWall = true;
                /*}
            }*/
        }
    }

    //Acts on entry of collider
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SUCC")
        {
            inSucc = true;
        } else if (collision.gameObject.tag == "Player Attack") {//dies to player attacks without succ
      Die();
    } else
        {
            inSucc = false;
        }
    }
}
