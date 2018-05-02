using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerSturn : MonoBehaviour
{
    public GameObject _SturnEffect;
    public static CPlayerSturn _instance = null;

    public bool isSturn;
    


    private void Awake()
    {
        CPlayerSturn._instance = this;
    }
	
	void Update ()
    {
        Sturn();
    }

    void Sturn()
    {
        if (!isSturn)
        {
            StopCoroutine("SturnCoolTime");
            return;
        }

        StartCoroutine("SturnCoolTime");
    }


    IEnumerator SturnCoolTime()
    {
        SturnOn();
        yield return new WaitForSeconds(InspectorManager._InspectorManager.fSturnTime);
        SturnOff();
    }

    void SturnOn()
    {
        _SturnEffect.SetActive(true); // 스턴이펙트
        // 플레이어 움직임막아줌
        CPlayerManager._instance._CPlayerAniEvent.MoveTypes(1); 
        CPlayerManager._instance.m_isRotationAttack = false;
    }
    void SturnOff()
    {
        _SturnEffect.SetActive(false); // 스턴이펙트 해제
        // 플레이어 움직임막아줌
        CPlayerManager._instance._CPlayerAniEvent.MoveTypes(2);
        CPlayerManager._instance.m_isRotationAttack = true;
        isSturn = false;
    }
}
