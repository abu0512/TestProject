using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerSwordCollder : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss" || other.tag == "Guard" || other.tag == "Queen" || other.tag == "ShildMushroom")
        {
            int nCombo = CPlayerManager._instance.m_nAttackCombo - 1;
            if (nCombo == -1) nCombo = 1;

            if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
            {

                if (other.tag == "Guard")
                {
                    other.GetComponent<MonsterBase>().isHit = true;
                    other.GetComponent<GuardMushroomEffect>().GuardMHitEffect();
                    other.GetComponent<GuardMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeShild[nCombo]);
                }

                else if (other.tag == "Queen")
                {
                    other.GetComponent<MonsterBase>().isHit = true;
                    other.GetComponent<QueenMushroomEffect>().QueenMHitEffect();
                    other.GetComponent<QueenMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeShild[nCombo]);
                }

                else if (other.tag == "ShildMushroom")
                {
                    if (other.GetComponent<ShildMushroom>().PlayerisFront == false)
                    {
                        other.GetComponent<ShildMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeShild[nCombo], InspectorManager._InspectorManager.nGroggyShild[nCombo]);
                        other.GetComponent<ShildMushroomEffect>().ShildMHitEffect();
                    }
                }

                else
                {
                    other.GetComponent<WitchBossEffect>().OnShieldEffect(nCombo);
                    other.GetComponent<WitchBoss>().OnDamage(InspectorManager._InspectorManager.nDamgeShild[nCombo], InspectorManager._InspectorManager.nGroggyShild[nCombo]);
                }
            }
            else
            {
                if (other.tag == "Guard")
                {
                    other.GetComponent<MonsterBase>().isHit = true;
                    other.GetComponent<GuardMushroomEffect>().GuardMHitEffect();
                    other.GetComponent<GuardMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeScythe[nCombo]);
                    CPlayerManager._instance.m_ScyPlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
                }

                else if (other.tag == "Queen")
                {
                    other.GetComponent<MonsterBase>().isHit = true;
                    other.GetComponent<QueenMushroomEffect>().QueenMHitEffect();
                    other.GetComponent<QueenMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeScythe[nCombo]);
                    CPlayerManager._instance.m_ScyPlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
                }

                else if (other.tag == "ShildMushroom")
                {
                    if (other.GetComponent<ShildMushroom>().PlayerisFront == false)
                    {
                        other.GetComponent<ShildMushroomEffect>().ShildMHitEffect();
                        CPlayerManager._instance.m_ScyPlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
                        other.GetComponent<ShildMushroom>().OnDamage(InspectorManager._InspectorManager.nDamgeScythe[nCombo], InspectorManager._InspectorManager.nGroggyScythe[nCombo]);
                    }
                }

                else
                {
                    other.GetComponent<WitchBossEffect>().OnScytheEffect(nCombo);
                    CPlayerManager._instance.m_ScyPlayerHp += InspectorManager._InspectorManager.fScytheAttackHpAdd;
                    other.GetComponent<WitchBoss>().OnDamage(InspectorManager._InspectorManager.nDamgeScythe[nCombo], InspectorManager._InspectorManager.nGroggyScythe[nCombo]);
                }
            }
            CPlayerManager._instance._nPowerGauge += InspectorManager._InspectorManager.nPlayerHitAddPower;
            if (nCombo == 0) CPlayerManager._instance.PlayerHitCamera(3.5f, 0.1f);
            if (nCombo == 1) CPlayerManager._instance.PlayerHitCamera(3.2f, 0.1f);
            if (nCombo == 2) CPlayerManager._instance.PlayerHitCamera(2.8f, 0.1f);
        }
    }
}