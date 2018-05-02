using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuardMushroomAttack : GuardMushroomStateBase
{
    
    public override void BeginState()
    {
        Dltime = 0f;
    }

    public override void EndState()
    {
        base.EndState();
    }
       
    public void AttackCheck()
    {
        if (GuardMushroom.GetDistanceFromPlayer() < GuardMushroom.MStat.AttackDistance + 1.5f 
            && GuardMushroom.PlayerisFront)
        {
            if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Defense_ModeIdle)
            {
                    CPlayerManager._instance.PlayerHp(0.2f, 2, GuardMushroom.AttackDamage);
            }

            else
            {
                    CPlayerManager._instance.PlayerHp(0.2f, 1, GuardMushroom.AttackDamage);
            }
        }
    }

    void Update()
    {
        GuardMushroom.GoToPullPush();
        GuardMushroom.NowisHit();
        GuardMushroom.PlayerisDead();
        GuardMushroom.QueenisADead();

        Dltime += Time.deltaTime;

        if (Dltime > 1.5f)
        {
            if (GuardMushroom.GetDistanceFromPlayer() > GuardMushroom.MStat.AttackDistance)
            {
                GuardMushroom.SetState(GuardMushroomState.Chase);
                Dltime = 0;
                GuardMushroom.AttackTimer = 0f;
                return;
            }

            else
            {
                GuardMushroom.SetState(GuardMushroomState.Return);
                Dltime = 0;
                GuardMushroom.AttackTimer = 0f;
                return;
            }
        }
    }
}
