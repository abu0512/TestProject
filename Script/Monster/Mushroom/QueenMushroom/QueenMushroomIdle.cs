using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomIdle : QueenMushroomStateBase
{
    public override void BeginState()
    {
    }

    public override void EndState()
    {
    }

    void Start()
    {
        QueenMushroom.AttackTimer = 0f;
    }

    void Update()
    {
        QueenMushroom.GoToPullPush();

        if (QueenMushroom.GetDistanceFromPlayer() < QueenMushroom.Stat.ChaseDistance && CPlayerManager._instance.isDead == false)
        {
            QueenMushroom.GoToDestination(QueenMushroom.Player.position, QueenMushroom.Stat.MoveSpeed, QueenMushroom.rotAnglePerSecond);
            QueenMushroom.SetState(QueenMushroomState.Chase);
            return;
        }
    }
}
