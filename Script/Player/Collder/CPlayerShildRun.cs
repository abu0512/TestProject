using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerShildRun : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss" || other.tag == "Guard" || other.tag == "Queen")
        {
            CPlayerManager._instance.PlayerHitCamera(2.5f, 0.2f);
            if (other.tag == "Guard")
            {
                other.GetComponent<MonsterBase>().isHit = true;
                other.GetComponent<GuardMushroom>().OnDamage(InspectorManager._InspectorManager.fShildRunDamge);
            }
            else if (other.tag == "Queen")
            {
                other.GetComponent<MonsterBase>().isHit = true;
                other.GetComponent<QueenMushroom>().OnDamage(InspectorManager._InspectorManager.fShildRunDamge);
            }
            else
            {
                other.GetComponent<WitchBoss>().OnDamage(InspectorManager._InspectorManager.fShildRunDamge);
            }
            CPlayerAttackEffect._instance.Effect8();
            CPlayerManager._instance._PlayerAni_Contorl.AniStiff();
        }
    }
}
