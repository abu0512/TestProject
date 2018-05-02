using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerSword : CPlayerBase
{
    public BoxCollider _ScytheCollder;
    public BoxCollider _SowrdCollder;
    public BoxCollider _CounteCollder;
    public BoxCollider _StartScytheSkillCollder;
    public BoxCollider _ShildRunCollder;

    private bool m_bCollder;
    private float m_fCollderTimer;
    

    void Start()
    {
        _ScytheCollder.enabled = false;
        _SowrdCollder.enabled = false;

        m_bCollder = false;
        m_fCollderTimer = 0;
    }
    void Update()
    {
        if(m_bCollder)
        {
            m_fCollderTimer += Time.deltaTime;

            if(m_fCollderTimer >= 0.1f)
            {
                return;
            }

            m_fCollderTimer = 0;
            m_bCollder = false;
            CollderFalse();
        }
    }
    public void ScytheTrue()
    {
        _ScytheCollder.enabled = true;
        m_bCollder = true;
    }
    public void SowrdTrue()
    {
        _SowrdCollder.enabled = true;
        m_bCollder = true;
    }
    public void CounterTrue()
    {
        _CounteCollder.enabled = true;
        m_bCollder = true;
    }
    public void ScytheSkill1()
    {
        _StartScytheSkillCollder.enabled = true;
        m_bCollder = true;
    }
    public void ShildRun(int type)
    {
        if (type == 1)
            _ShildRunCollder.enabled = true;
        else if (type == 2)
            _ShildRunCollder.enabled = false;
    }
    public void CollderFalse()
    {
        _SowrdCollder.enabled = false;
        _ScytheCollder.enabled = false;
        _CounteCollder.enabled = false;
        _StartScytheSkillCollder.enabled = false;
        _ShildRunCollder.enabled = false;
    }
}
