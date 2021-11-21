using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirKibbleAI : MonoBehaviour
{
    [SerializeField] float jumpHeight;
    [SerializeField] Sprite[] aiSprite;
    [SerializeField] Transform playerTrans;
    [SerializeField] float attackRange;

    Transform trans;
    Rigidbody2D body;
    SpriteRenderer sprite;

    int _health = 10;

    bool isGrounded;
    bool inSucc;
    bool inCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inSucc)
        {
            _health -= 5;
            inSucc = false;
        }
        if (_health < 1)
        {
            Die();
        }
    }

    // Update is called regularly
    void FixedUpdate()
    {
        Attack();
    }

    //Destroys entity
    void Die()
    {
        Destroy(gameObject);
    }

    //Runs Kibble's attack cycle
    void Attack()
    {
        if (Mathf.Abs(playerTrans.position.x - transform.position.x) < attackRange &&
            Mathf.Abs(playerTrans.position.y - transform.position.y) < attackRange)
        {
            if (!inCoolDown) 
            {
                Jump();
            }
        }
    }

    //Makes entity jump
    void Jump()
    {
        if (isGrounded) {
            body.velocity = new Vector2(0, jumpHeight);
            isGrounded = false;
        }
    }

    //Returns AI's health
    public int GetHealth()
    {
        return _health;
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
    }

    //Acts on entry of collider
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SUCC")
        {
            inSucc = true;
        }
        else
        {
            inSucc = false;
        }
    }
}
