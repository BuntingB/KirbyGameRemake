using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float immuneTime = 0;
    float immuneDelay = 0.5f;

    public int _health = 20;

    bool _tookDamage;
    bool _immune;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_health < 1) 
        {
            Die();
        }
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            TakeDamage(2);
        }
        if (immuneTime < Time.realtimeSinceStartup) 
        {
            immuneTime = Time.realtimeSinceStartup + immuneDelay;
            _immune = false;
        }
    }
    public int GetHealth() 
    {
        return _health;
    }
    public void TakeDamage(int damage)
    {
        if (_tookDamage && !_immune) 
        {
            _health -= damage;
            _immune = true;
        }
    }
    public void Die() 
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy") 
        {
            _tookDamage = true;
            TakeDamage(2);
        }
    }
}
