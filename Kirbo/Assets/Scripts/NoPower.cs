using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPower : Power
  {
  public NoPower(GameObject hitbox) {
    this.hitbox = hitbox;
    this.xOffSet = 0.16f;
    this.yOffSet = 0f;
    this.xScale = 0.23f;
    this.yScale = 0.14f;
  }
  override public void Attack() {
    hitbox.transform.localScale = new Vector3(xScale, yScale, 1);
    hitbox.transform.localPosition = new Vector2(xOffSet, yOffSet);
  }
  public override void NoAttack() {
    hitbox.transform.position = new Vector2(-20, 20);
  }
}
