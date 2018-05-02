using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomChase : QueenMushroomStateBase
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
        QueenMushroom.GoToPullPush();
        QueenMushroom.PlayerisDead();
        QueenMushroom.TimeToHeal();

        QueenMushroom.GoToDestination(QueenMushroom.Player.position, QueenMushroom.MStat.MoveSpeed, QueenMushroom.rotAnglePerSecond);

        if (QueenMushroom.GetDistanceFromPlayer() < QueenMushroom.MStat.AttackDistance)
        {
            QueenMushroom.GoToPullPush();

            if (QueenMushroom.AttackTimer > QueenMushroom.AttackDelay)
            {
                QueenMushroom.SetState(QueenMushroomState.Attack);
                return;
            }
        }

        else
        {
            QueenMushroom.SetState(QueenMushroomState.Return);
            return;
        }
    }
}
