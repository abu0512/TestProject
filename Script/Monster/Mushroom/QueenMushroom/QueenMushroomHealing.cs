using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomHealing : QueenMushroomStateBase
{

    public override void BeginState()
    {
       QueenMushroom.EffectofHeal(transform.position);
        Dltime = 0;
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void HealCheck()
    {
        QueenMushroom.HealStart = true;
        QueenMushroom.HealTimer = 0;
    }

    void Update()
    {
        QueenMushroom.GoToPullPush();
        Dltime += Time.deltaTime;
        QueenMushroom.GoToDestination(transform.position, 0, 0);

        if (Dltime > 2f)
        {
            QueenMushroom.HealEffect.SetActive(false);
            QueenMushroom.SetState(QueenMushroomState.Chase);
            return;
        }
    }
}
