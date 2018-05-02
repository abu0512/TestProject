using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMushroomPP : QueenMushroomStateBase
{
    public override void BeginState()
    {
        Dltime = 0;
    }

    public override void EndState()
    {
        base.EndState();
    }

    void Update()
    {
        QueenMushroom.ModeChange();
        Dltime += Time.deltaTime;

        if (Dltime > 0.8f)
        {
            CPlayerManager._instance.isPush = false;
            CPlayerManager._instance.isPull = false;
            QueenMushroom.PPEnding = true;

            QueenMushroom.SetState(QueenMushroomState.Chase);
            return;
        }
    }
}
