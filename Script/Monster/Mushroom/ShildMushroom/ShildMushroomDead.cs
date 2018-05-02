using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildMushroomDead : ShildMushroomStateBase
{
    private float DeadTime;

    public override void BeginState()
    {
        DeadTime = 0;
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Update()
    {
        if (ShildMushroom.isDead)
        {
            ShildMushroom.rotAnglePerSecond = 0;
            ShildMushroom.Stat.MoveSpeed = 0;
            DeadTime += Time.deltaTime;
            ShildMushroom.CharacterisDead = true;

            if (DeadTime >= 1.6f)
            {
                ShildMushroom.OnDead();
                return;
            }
        }
    }
}
