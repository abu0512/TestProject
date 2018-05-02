using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerCountAttack : CPlayerBase
{
    public bool isCount;

    private void Start()
    {
        isCount = false;
    }
    void Update ()
    {
        CountDirect();
    }

    void CountDirect()
    {
        if (!isCount)
            return;

        //if(isCount)
        //{
        //    _PlayerManager.PlayerHitCamera(1.5f);
        //    for (float i = 0; i < 1.0f; i += 0.001f)
        //    {
        //        TimeScalManager._instance.TimeScal(i);
        //        if (i >= 0.99f)
        //        {
        //            TimeScalManager._instance.TimeScal(1.0f);
        //            isCount = false;
        //        }
        //    }
        //}
    }



}
