using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorManager : MonoBehaviour
{
    public static InspectorManager _InspectorManager = null;

    public float fStmAuto;
    public float fStmAutoTime;
    public float fStmDash;

    public float fRotation;
    public float fMoveSpeed;
    public float fMoveAngle;

    public int[] nDamgeShild = new int[3];
    public int[] nDamgeScythe = new int[3];
    public float[] nGroggyShild = new float[3];
    public float[] nGroggyScythe = new float[3];

    public float fShildDamge;
    public float fSwapCoolTime;

    public float fCountGroggy;
    public float fCountAttackDamge;
    public float fCountAttackReturnTime;

    public float fScytheTime;
    public float fScytheTimeHpDown;

    public float fScytheTimeHpExponential;
    public float fScytheAttackHpAdd;

    public float fScytheStartSkillDamge;
    public float fScytheSkill2Damge;

    public float fSturnTime;

    public float fPlayerAttackStiff;
    public float fSweatStm;
    public float fShildRunDamge;
    public float fShildRunStm;

    public float fPlayerHornTime;
    public int nPlayerHitAddPower;
    public float fPlayerSweatCountTime; // 흘리기후 몇초동안 반격할수있는 시간을 유지하는지
    public float fLockOnSpeed;




    void Awake ()
    {
        _InspectorManager = this;
        StartCoroutine("StartAutoStm");
    }

    IEnumerator StartAutoStm()
    {
        while(true)
        {
            yield return new WaitForSeconds(fStmAutoTime);
            if(CPlayerManager._instance.m_PlayerStm < 100)
                CPlayerManager._instance.m_PlayerStm += fStmAuto;
        }
    }
}
