using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomDead : GuardMushroomStateBase
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
        if (GuardMushroom.isDead)
        {
            GuardMushroom.rotAnglePerSecond = 0;
            GuardMushroom.MStat.MoveSpeed = 0;
            DeadTime += Time.deltaTime;
            GuardMushroom.CharacterisDead = true;

            if (DeadTime >= 1.6f)
            {
                GuardMushroom.OnDead();
                return;
            }
        }
    }
}
