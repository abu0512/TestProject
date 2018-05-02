using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildMushroomAttack : ShildMushroomStateBase
{
    public override void BeginState()
    {
        Dltime = 0f;
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void ShildAttackCheck()
    {
        if (ShildMushroom.GetDistanceFromPlayer() < ShildMushroom.Stat.AttackDistance + 1.5f
            && ShildMushroom.PlayerisFront)
        { 
            if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Defense_ModeIdle)
            {
                CPlayerManager._instance.PlayerHp(0.2f, 2, ShildMushroom.AttackDamage);
            }

            else
            {
                CPlayerManager._instance.PlayerHp(0.2f, 1, ShildMushroom.AttackDamage);
            }
        }
    }

    void Update()
    {
        ShildMushroom.GroggyCheck();
        ShildMushroom.PlayerisDead();
        Dltime += Time.deltaTime;

        if (Dltime > 1.5f)
        {
            if (ShildMushroom.GetDistanceFromPlayer() > ShildMushroom.Stat.AttackDistance)
            {
                ShildMushroom.SetState(ShildMushroomState.Chase);
                Dltime = 0;
                ShildMushroom.AttackTimer = 0f;
                return;
            }

            else
            {
                ShildMushroom.SetState(ShildMushroomState.Return);
                Dltime = 0;
                ShildMushroom.AttackTimer = 0f;
                return;
            }
        }
    }
}
