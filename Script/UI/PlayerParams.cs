using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerParams : CharacterUI
{
    public static PlayerParams _instance = null;

    protected CPlayerManager _CPlayerManager;
    protected CPlayerManager CPlayerManager { get { return _CPlayerManager; } set { _CPlayerManager = value; } }

    public string names { get; set; }
    public Image HPBar;
    public Image SPBar;
    public Image[] GaugeBar;
    public GameObject[] PlayerType;
    public GameObject[] PlayerWType;

    public int nGauge;
    private float ScycurHP;
    private float ScymaxHP;
    private float PowerGauge;

    public override void InitParams()
    {
        PlayerParams._instance = this;

        names = "Player";
        maxHP = CPlayerManager._instance.m_PlayerMaxHp;
        curHP = CPlayerManager._instance.m_PlayerHp;
        ScymaxHP = CPlayerManager._instance.m_ScyPlayerMaxHp;
        maxSP = CPlayerManager._instance.m_PlayerMaxStm;
        curSP = maxSP;
        PowerGauge = CPlayerManager._instance._nPowerGauge;
    }

    private void Awake()
    {
        HPBar = GameObject.FindGameObjectWithTag("HP").GetComponentInChildren<Image>();
        SPBar = GameObject.FindGameObjectWithTag("Stm").GetComponentInChildren<Image>();
    }

    public void SetHp()
    {
        curHP = CPlayerManager._instance.m_PlayerHp;
        curHP = Mathf.Clamp(curHP, 0, maxHP);
    }

    public void SetScyHP()
    {
        ScycurHP = CPlayerManager._instance.m_ScyPlayerHp;
        ScycurHP = Mathf.Clamp(ScycurHP, 0, ScymaxHP);
    }

    public void HPlocalScale()
    {
        if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
        {
            HPBar.fillAmount = curHP / maxHP;
        }

        else
        {
            HPBar.fillAmount = ScycurHP / ScymaxHP;
        }
    }

    public void GaugelocalScale()
    {
        PowerGauge = CPlayerManager._instance._nPowerGauge;
        PowerGauge = Mathf.Clamp(PowerGauge, 0, 300);

        if (PowerGauge <= 50)
        {
            GaugeBar[0].fillAmount = PowerGauge / 50;
            for(int j = 1; j < GaugeBar.Length; j++)
            {
                GaugeBar[j].fillAmount = 0;
            }
        }
        else if (50 < PowerGauge && PowerGauge <= 100)
        {
            float i = PowerGauge - 50;
            GaugeBar[0].fillAmount = 1 / 1;
            GaugeBar[1].fillAmount = i / 50;
            for (int j = 2; j < GaugeBar.Length; j++)
            {
                GaugeBar[j].fillAmount = 0;
            }
        }
        else if (100 < PowerGauge && PowerGauge <= 150)
        {
            float i = PowerGauge - 100;

            GaugeBar[0].fillAmount = 1 / 1;
            GaugeBar[1].fillAmount = 1 / 1;
            GaugeBar[2].fillAmount = i / 50;
            for (int j = 3; j < GaugeBar.Length; j++)
            {
                GaugeBar[j].fillAmount = 0;
            }
        }
        else if (150 < PowerGauge && PowerGauge <= 200)
        {
            float i = PowerGauge - 150;

            GaugeBar[0].fillAmount = 1 / 1;
            GaugeBar[1].fillAmount = 1 / 1;
            GaugeBar[2].fillAmount = 1 / 1;
            GaugeBar[3].fillAmount = i / 50;
            for (int j = 4; j < GaugeBar.Length; j++)
            {
                GaugeBar[j].fillAmount = 0;
            }
        }
        else if (200 < PowerGauge && PowerGauge <= 250)
        {
            float i = PowerGauge - 200;

            GaugeBar[0].fillAmount = 1 / 1;
            GaugeBar[1].fillAmount = 1 / 1;
            GaugeBar[2].fillAmount = 1 / 1;
            GaugeBar[3].fillAmount = 1 / 1;
            GaugeBar[4].fillAmount = i / 50;
            for (int j = 5; j < GaugeBar.Length; j++)
            {
                GaugeBar[j].fillAmount = 0;
            }
        }
        else if (250 < PowerGauge && PowerGauge <= 300)
        {
            float i = PowerGauge - 250;

            GaugeBar[0].fillAmount = 1 / 1;
            GaugeBar[1].fillAmount = 1 / 1;
            GaugeBar[2].fillAmount = 1 / 1;
            GaugeBar[3].fillAmount = 1 / 1;
            GaugeBar[4].fillAmount = 1 / 1;
            GaugeBar[5].fillAmount = i / 50;
        }
    }

    public void SetSp()
    {
        curSP = CPlayerManager._instance.m_PlayerStm;
        curSP = Mathf.Clamp(curSP, 0, maxSP);
    }

    public void SPlocalScale()
    {
        SPBar.fillAmount = curSP / maxSP;
    }

    public void SetPlayerType()
    {
        if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
        {
            PlayerType[1].SetActive(false);
            PlayerWType[1].SetActive(false);
            PlayerType[0].SetActive(true);
            PlayerWType[0].SetActive(true);
        }

        else
        {
            PlayerType[1].SetActive(true);
            PlayerWType[1].SetActive(true);
            PlayerType[0].SetActive(false);
            PlayerWType[0].SetActive(false);
        }
    }

    public void GaugeOff()
    {
        CPlayerManager._instance._nPowerGauge = 0;
    }

    void Update()
    {
        // Player 캐릭터의 체력과 스테미너의 값을 받아온다.
        SetHp();
        SetScyHP();
        SetSp();

        // player 캐릭터 HP bar 실시간 UI 상태 변화
        HPlocalScale();

        // player 캐릭터 스테미너 bar 실시간 UI 상태 변화
        SPlocalScale();

        // Player 캐릭터 타입 + 무기 타입 실시간 상태 변화
        SetPlayerType();

        // Player 캐릭터 파워 게이지 Bar 실시간 UI 상태 변화
        GaugelocalScale();
    }
}
