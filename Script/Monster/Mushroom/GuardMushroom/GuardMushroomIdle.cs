﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomIdle : GuardMushroomStateBase
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
        GuardMushroom.GoToPullPush();
        GuardMushroom.NowisHit();

        if (GuardMushroom.GetDistanceFromPlayer() < GuardMushroom.MStat.ChaseDistance && (CPlayerManager._instance.isDead == false))
        {
            GuardMushroom.GoToDestination(GuardMushroom.Player.position, GuardMushroom.MStat.MoveSpeed, GuardMushroom.rotAnglePerSecond);
            GuardMushroom.SetState(GuardMushroomState.Chase);
            return;
        }
    }
}
