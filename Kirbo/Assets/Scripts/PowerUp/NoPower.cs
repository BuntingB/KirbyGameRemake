using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPower : Power
  {
  public NoPower(GameObject hitbox) {
    this.hitbox = hitbox;
    this.xOffSet = 0.1f;
    this.yOffSet = 0f;
    this.xScale = 0.23f;
    this.yScale = 0.14f;
  }
  private void Start() {
    this.xOffSet = 0.16f;
    this.yOffSet = 0f;
    this.xScale = 0.23f;
    this.yScale = 0.14f;
  }
  public override void Attack() {
    hitbox.transform.localScale = new Vector3(xScale, yScale, 1);
    hitbox.transform.localPosition = new Vector2(xOffSet, yOffSet);
    anim.SetBool("Succ", true);
  }
  public override void NoAttack() {
    hitbox.transform.position = new Vector3(-20, 20, 1);
    anim.SetBool("Succ", false);
  }
}
