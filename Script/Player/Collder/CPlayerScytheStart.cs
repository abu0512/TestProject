using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerScytheStart : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //CPlayerManager._instance._PlayerAni_Contorl.InteractionOn
        if (other.tag == "Boss" || other.tag == "Guard" || other.tag == "Queen")
        {
            CPlayerManager._instance.PlayerHitCamera(3.0f, 0.2f);
            float SkillDamge1 = InspectorManager._InspectorManager.fScytheStartSkillDamge;
            float SkillDamge2 = InspectorManager._InspectorManager.fScytheSkill2Damge;
            int PowerGauge = PlayerParams._instance.nGauge;

            float Damge1 = SkillDamge1 + (SkillDamge1 * PowerGauge / 6);
            float Damge2 = SkillDamge2 + (SkillDamge2 * PowerGauge / 6);


            if (other.tag == "Guard")
            {
                other.GetComponent<MonsterBase>().isHit = true;
                other.GetComponent<GuardMushroomEffect>().GuardSwapEffect();

                if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Scythe == PlayerAni_State_Scythe.Skill2)
                    other.GetComponent<GuardMushroom>().OnDamage(Damge2);
                else
                    other.GetComponent<GuardMushroom>().OnDamage(Damge1);
            }

            else if (other.tag == "Queen")
            {
                other.GetComponent<MonsterBase>().isHit = true;
                other.GetComponent<QueenMushroomEffect>().QueenSwapEffect();

                if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Scythe == PlayerAni_State_Scythe.Skill2)
                    other.GetComponent<QueenMushroom>().OnDamage(Damge2);
                else
                    other.GetComponent<QueenMushroom>().OnDamage(Damge1);
            }

            else
            {
                if (CPlayerManager._instance._PlayerAni_Contorl._PlayerAni_State_Scythe == PlayerAni_State_Scythe.Skill2)
                    other.GetComponent<WitchBoss>().OnDamage(Damge2);
                else
                    other.GetComponent<WitchBoss>().OnDamage(Damge1);
            }
            CPlayerManager._instance._PlayerAni_Contorl.AniStiff();
            PlayerParams._instance.nGauge = 0;
            PlayerParams._instance.GaugeOff();
            CPlayerManager._instance._nPowerGauge = 0;

        }
    }
}

