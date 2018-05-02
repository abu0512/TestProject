using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomReturn : QueenMushroomStateBase
{
    public override void BeginState()
    {
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

        if (QueenMushroom.GetDistanceFromPlayer() < QueenMushroom.MStat.AttackDistance)
        {
            if (QueenMushroom.AttackTimer > QueenMushroom.AttackDelay)
            {
                QueenMushroom.SetState(QueenMushroomState.Attack);
                return;
            }
        }

        if (QueenMushroom.GetDistanceFromPlayer() < QueenMushroom.MStat.ChaseDistance && QueenMushroom.GetDistanceFromPlayer() > QueenMushroom.MStat.AttackDistance)
        {
            QueenMushroom.SetState(QueenMushroomState.Chase);
            return;
        }
    }
}
