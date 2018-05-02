using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomisHit : GuardMushroomStateBase
{
    public override void BeginState()
    {
        Dltime = 0;
    }

    public override void EndState()
    {
        base.EndState();
        GuardMushroom.AttackTimer = 1.3f;
    }

    void Update()
    {
        GuardMushroom.NowisHit();
        GuardMushroom.GoToPullPush();
        GuardMushroom.QueenisADead();

        Dltime += Time.deltaTime;

        if (Dltime > 0.15f)
        {
            if (GuardMushroom.QueenisAllDead)
            {
                GuardMushroom.SetState(GuardMushroomState.BChase);
                return;
            }

            else if(GuardMushroom.SBombing)
            {
                GuardMushroom.SetState(GuardMushroomState.Sbombing);
                return;
            }

            else
            {
                GuardMushroom.SetState(GuardMushroomState.Return);
                return;
            }
        }
    }
}
