using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomChase : GuardMushroomStateBase
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
        GuardMushroom.ModeChange();
        GuardMushroom.NowisHit();
        GuardMushroom.PlayerisDead();
        GuardMushroom.QueenisADead();

        GuardMushroom.GoToDestination(GuardMushroom.Player.position, GuardMushroom.MStat.MoveSpeed, GuardMushroom.rotAnglePerSecond);

        if (GuardMushroom.GetDistanceFromPlayer() < GuardMushroom.MStat.AttackDistance)
        {
            if (GuardMushroom.AttackTimer > GuardMushroom.AttackDelay)
            {
                GuardMushroom.SetState(GuardMushroomState.Attack);
                return;
            }
        }

        if (GuardMushroom.AttackTimer < GuardMushroom.AttackDelay && GuardMushroom.GetDistanceFromPlayer() <= GuardMushroom.MStat.AttackDistance)
        {
            GuardMushroom.SetState(GuardMushroomState.Return);
            return;
        }
    }
}
