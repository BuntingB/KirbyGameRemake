using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
  [SerializeField] Image mask;
  [SerializeField] GameObject playerObj;

  Player playerProperties;
  float maxHealth = 20;

  // Start is called before the first frame update
  void Start()
  {
    playerProperties = playerObj.GetComponent<Player>();
  }

  // Update is called once per frame
  void Update()
  {
    float fillAmount = (float)playerProperties.GetHealth() / (float)maxHealth;
    mask.fillAmount = fillAmount;
  } 
}
