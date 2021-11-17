using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Power : MonoBehaviour
{
  public float xScale, yScale;
  public float xOffSet, yOffSet;
  public GameObject hitbox;
  //abstract Power();
  public abstract void Attack();
  public abstract void NoAttack();
}
