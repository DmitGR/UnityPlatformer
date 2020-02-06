using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet {

    protected override void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        if (direction.x < 0)
            spriteRender.flipX = true;
        else spriteRender.flipX = false;
        base.Update();
    }
}
