
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerShild : CPlayerBase
{
    public BoxCollider _BoxCollider;
    public GameObject _ShildEffect;
    public bool m_bShildCollider;
    private float m_fShildTimer;

    [SerializeField]
    private bool isShildCounter;
    public bool _isShildCounter { get { return isShildCounter; } set { value = isShildCounter; } }

    public bool isCounterTimer;
    private float fCounterTime;

    void Start () {
        _BoxCollider.enabled = false;
        m_bShildCollider = false;
        m_fShildTimer = 0;
        fCounterTime = 0.1f;
    }
	
	
	void Update ()
    {
        ShildCheck(); 
        if (_PlayerManager._PlayerAni_Contorl._PlayerAni_State_Shild == PlayerAni_State_Shild.Defense_ModeIdle)
        {
            _BoxCollider.enabled = true;
        }
        else
            _BoxCollider.enabled = false;

        if(isCounterTimer)
        {
            fCounterTime += Time.deltaTime;

            if(fCounterTime <= 0)
                TimeScalManager._instance.TimeScal(0.01f);

            for (float i = 0; i < 1; i += 0.05f)
                TimeScalManager._instance.TimeScal(i);

            if (fCounterTime > 1.0f)
            {
                TimeScalManager._instance.TimeScal(1.0f);
                isCounterTimer = false;
            }
        }
    }
    void ShildCheck()
    {
        if (!m_bShildCollider)
        {
            m_fShildTimer = 0;
            _PlayerManager._PlayerAni_Contorl.m_bDefenseBack = false;
            _ShildEffect.SetActive(false);
            return;
        }

        if (m_bShildCollider)
        {            
            _ShildEffect.SetActive(true);
            m_fShildTimer += Time.deltaTime;
            _PlayerManager._PlayerAni_Contorl.m_bDefenseBack = true;            
            if (m_fShildTimer >= 0.3f)
            {
                m_bShildCollider = false;
            }
        }
    }

    public void CounterShild(int type)
    {
        if (type == 1)
        {            
            isShildCounter = true;            
        }
        if (type == 2)
            isShildCounter = false;
    }
    public void Counter()
    {
        Debug.Log("카운터 처리");
    }
}
