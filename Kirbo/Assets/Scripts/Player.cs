using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  int _health = 20;

  bool _tookDamage;

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
    if (_health < 1) {
      Die();
    }
    if (Input.GetKeyDown(KeyCode.E)) {
      TakeDamge(2);
    }
  }
  public int GetHealth() {
    return _health;
  }
  public void TakeDamge(int damage) {
    _health -= damage;
  }
  public void Die() {
    Destroy(gameObject);
  }
}
