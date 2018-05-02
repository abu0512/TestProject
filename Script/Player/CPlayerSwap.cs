using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 낫, 검방패 모드
public enum PlayerMode
{
    Shield = 0,
    Scythe,
}
public class CPlayerSwap : CPlayerBase
{
    public PlayerMode _PlayerMode = PlayerMode.Shield; // 처음엔 검방패로 시작

    // 플레이어 좌표 담아둠
    public Transform _Follow; 
    // _Shield 배열에 낫,검방패 오브젝트 넣어둠
    public GameObject[] _EffectModle;
    // 플레이어 텔포 이펙트
    public GameObject[] _EffectTelpo;
    

    public bool m_bSwapAttack;
    private float m_fSwapAttackTime;

    // 이펙트 on일때 사라지는 시간
    private bool isEffect;
    private bool isEffectTelpo;
    private float fEffectTime;

    // 블링크
    private float m_fDisMin;
    public float m_fMoveDir;
    private int nScytheNum;
    private int nScytheExponential;

    // 직업변경 쿨타임 
    private bool isCoolTimeSwap;
    public bool _isCoolTimeSwap { get { return isCoolTimeSwap; } set { value = isCoolTimeSwap; } }

    public bool isBlink;
    private float fBlinkTime;

    private bool isBlinkSwapKey;
    void Start()
    {
        m_fDisMin = 1.5f;
        m_fMoveDir = 5.0f;

        fEffectTime = 0;
        EffectModle(false, false);
        m_bSwapAttack = false;
        m_fSwapAttackTime = 0.0f;

        nScytheNum = 0;
        nScytheExponential = 1;

        isCoolTimeSwap = true;
        isBlink = false;
        isBlinkSwapKey = false;

    }
    void Update ()
    {
        SwapKey();
        EffectTimer();
        SwapAttacker();
        TelPoEffect();
        BlinkPlayer();
        if (Input.GetKeyDown(KeyCode.LeftShift) && _PlayerMode == PlayerMode.Scythe && _PlayerManager.m_PlayerStm > 30f)
        {
            CSwapSystem._instance.ObjSwap(false, false);
            CCameraFind._instance.BlinkCameraOn();
            RayCastChack();
            BlinkStart();
            _PlayerManager.m_PlayerStm -= 30f;
        }
    }

    void SwapKey()
    {
        // Q,E버튼을 누르면 직업이 바뀜
       
        if(isCoolTimeSwap)
        {
            if (_PlayerMode == PlayerMode.Shield)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {                     
                    ScytheReset(1);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    ScytheReset();
                }
            }
            else
            {
                if(!isBlinkSwapKey)
                {
                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
                    {
                        ShildReset();
                    }
                }
            }
        }
    }

    void EffectTimer()
    {
        if (!isEffect)
        {
            fEffectTime = 0;
            return;
        }

        if (isEffect)
        {
            fEffectTime += Time.deltaTime;
            if (fEffectTime >= 2.0f)
            {
                EffectModle(false, false);
                isEffect = false;
            }
        }
    }
    void TelPoEffect()
    {
        if (!isEffectTelpo)
        {
            return;
        }

        if(isEffectTelpo)
        {
            _EffectTelpo[0].SetActive(false);
            _EffectTelpo[0].SetActive(true);
            isEffectTelpo = false;
        }
    }
    void SwapAttacker()
    {
        if (!m_bSwapAttack)
        {
            m_fSwapAttackTime = 0;
            return;
        }

        if (m_bSwapAttack)
        {
            m_fSwapAttackTime += Time.deltaTime;
            _PlayerManager._PlayerAni_Contorl._PlayerAni_State_Scythe = PlayerAni_State_Scythe.Skill1;
            if (m_fSwapAttackTime >= 0.1f)
            {
                CPlayerManager._instance.PlayerHitCamera(2.0f);
            }
            if (m_fSwapAttackTime >= 0.4f)
            {
                m_bSwapAttack = false;
            }
        }
    }
    void EffectModle(bool a, bool b)
    {
        _EffectModle[0].SetActive(a);
        _EffectModle[1].SetActive(b);
    }
    public void RayCastChack()
    {
        isEffectTelpo = true;
        // 레이 쏠위치, 방향 -> 캐릭터는 계속 직선만 보기때문에 forward를 사용함
        Ray ray = new Ray(_Follow.transform.position, _Follow.transform.forward);
        // hit 
        RaycastHit hit = new RaycastHit();
        // 거리
        float dir;

        Debug.DrawRay(ray.origin, ray.direction, Color.red, Mathf.Infinity);

        // 레이캐스트를 통해 어느 객체에 맞았을 경우 (범위는 캐릭터 거리 이동만큼)
        if (Physics.Raycast(ray, out hit, m_fMoveDir))
        {
            // 거리 =  (현재좌표 - 레이캐스트 맞은 객체좌표) - m_fDisMin
            // 거리 구한값에서 m_fDisMin를 빼주는 이유
            // 벽에서 사용하면 hit좌표의 끝에 걸려 위로 올라가기때문에 벽보다 좀뒤의 좌표로 계산
            dir = Vector3.Distance(transform.position, hit.point) - m_fDisMin;
            // 현재거리가 m_fDisMin거리보다 작을경우 
            if (dir > m_fDisMin)
            {
                // 캐릭터의 위치를 dir 거리만큼 증가
                transform.position += transform.forward * dir;
            } // 아닐경우 함수호출중단
            else
                return;
        }
        else
        {
            transform.position += transform.forward * m_fMoveDir;
        }
    }
    
    void BlinkStart()
    {
        isBlinkSwapKey = true;
        _EffectTelpo[0].SetActive(false);
        _EffectTelpo[1].SetActive(false);
        isBlink = true;
        fBlinkTime = 0;
    }

    void BlinkPlayer()
    {
        if (!isBlink)
            return;

        fBlinkTime += Time.deltaTime;
        
        if (fBlinkTime > 0.4f)
        {
            isBlinkSwapKey = false;
            CSwapSystem._instance.ObjSwap(false, true);
            _EffectTelpo[1].SetActive(true);
        }
        if (fBlinkTime > 0.8f)
        {
            _EffectTelpo[0].SetActive(false);
        }
        if (fBlinkTime > 2.0f)
        {
            _EffectTelpo[1].SetActive(false);
            isBlink = false;
        }
    }

        

    void ScytheReset(int type = 0)
    {
        CSwapSystem._instance.ScytheObjs(type);
        EffectModle(false, true);
        StartCoroutine("ScytheHpDown");
        _PlayerMode = PlayerMode.Scythe;
        if (type == 1)
        {
            _PlayerManager.isPull = true;
            m_bSwapAttack = true;
        }
        Common();
    }
    void ShildReset()
    {
        _EffectTelpo[0].SetActive(false);
        _EffectTelpo[1].SetActive(false);
        _PlayerManager.PlayerHitCamera(3.5f, 0.8f);
        CSwapSystem._instance.ShildObjs();
        StopCoroutine("ScytheHpDown");
        nScytheNum = 0;
        nScytheExponential = 1;
        EffectModle(true, false);
        _PlayerMode = PlayerMode.Shield;
        _PlayerManager.isPush = true;
        isBlink = false;
        Common();
    }
    void Common()
    {
        _PlayerManager.SwapHpType((int)_PlayerMode + 1);
        _PlayerManager._CPlayerAniEvent.MoveOn();
        isEffect = true;
        isCoolTimeSwap = false;
        StartCoroutine("CoolTimeSwap");
    }


    IEnumerator ScytheHpDown()
    {
        while(true)
        {
            float hp = InspectorManager._InspectorManager.fScytheTimeHpDown;
            yield return new WaitForSeconds(InspectorManager._InspectorManager.fScytheTime);

            nScytheNum++;
            if(nScytheNum >= InspectorManager._InspectorManager.fScytheTimeHpExponential)
            {
                nScytheExponential = nScytheExponential * 2;
                nScytheNum = 0;
            }
            else
            {
                _PlayerManager.PlayerHp(0, 1, hp * nScytheExponential);
            }
        }
    }
    IEnumerator CoolTimeSwap()
    {
        yield return new WaitForSeconds(InspectorManager._InspectorManager.fSwapCoolTime);        
        isCoolTimeSwap = true;
    }
}

