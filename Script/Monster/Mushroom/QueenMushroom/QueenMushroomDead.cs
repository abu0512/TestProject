using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomDead : QueenMushroomStateBase
{
    public override void BeginState()
    {
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Update()
    {
        if (QueenMushroom.isDead)
        {
            QueenMushroom.OnDead();
            return;
        }
    }
}
