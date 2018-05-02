﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WitchState
{
    Idle = 0,
    Run,    
    Chase,
    Skill,
    Attack,
    Groggy,
    GuardAttack,
    GroggyRelease,
    AttackRelease,
    MonsterSpawn,
}

[RequireComponent(typeof(WitchStateSystem))]
public class WitchBoss : MonsterBase
{
    private WitchStateSystem _stateSystem;
    private WitchSkillSystem _skillSystem;
    private WitchState _curState;
    private int _phase;
    private CPlayerManager _player;
    private float _receiveDamage;
    private float _receiveDamage2;
    private bool _coroutineOn;
    public bool _isAttacking;
    [SerializeField]
    private float _closeDistance;
    private bool _closeTelCheck;
    private bool _telAttack;
    private float _groggyValue;
    private float _groggyInitTime;
    private bool _animDelay;
    private float _animDelayTime;
    private bool _isTel;
    private float _footHoldTime;
    private float _spawnTime;
    private bool _footHoldOn;
    private WitchMonsterSpawnerObject _spawner;
    private bool _isGravity;

    private WitchWeaponCollider _collider;

    [SerializeField]
    protected Material _oldMat; // 원래 매터리얼
    [SerializeField]
    protected Material _damageMat; // 맞았을 때 매터리얼
    public GameObject _bossMat;

    [SerializeField]
    private int _attackIdx;

    // properties
    public WitchState State { get { return _curState; } set { _curState = value; } }
    public int Phase { get { return _phase; } set { _phase = value; } }
    public WitchStateSystem StateSystem { get { return _stateSystem; } }
    public WitchSkillSystem SkillSys { get { return _skillSystem; } }
    public CPlayerManager Target { get { return _player; } }
    public float ReceiveDamage { get { return _receiveDamage; } set { _receiveDamage = value; } }
    public float ReceiveDamage2 { get { return _receiveDamage2; } set { _receiveDamage2 = value; } }
    public bool IsAttacking { get { return _isAttacking; } }
    public float CloseDistance { get { return _closeDistance; } }
    public WitchWeaponCollider Collider { get { return _collider; } }
    public bool CloseTelCheck { get { return _closeTelCheck; } set { _closeTelCheck = value; } }
    public bool TelAttack { get { return _telAttack; } set { _telAttack = value; } }
    public bool IsTel { get { return _isTel; } set { _isTel = value; } }
    public bool IsGravity { get { return _isGravity; } set { _isGravity = value; } }
    public int AttackIdx { get { return _attackIdx; } }

    protected override void Awake()
    {
        base.Awake();
        _stateSystem = GetComponent<WitchStateSystem>();
        _stateSystem.Witch = this;
        _skillSystem = GetComponent<WitchSkillSystem>();
        _skillSystem.Witch = this;
        _curState = WitchState.Groggy;
        _player = FindObjectOfType<CPlayerManager>();
        _receiveDamage = 0.0f;
        _collider = FindObjectOfType<WitchWeaponCollider>();
        //_footHoldSpawnTime = WitchValueManager.I.FootholdContinueTime;
        _spawner = FindObjectOfType<WitchMonsterSpawnerObject>();
        _isGravity = true;
    }

    void Start ()
    {
        _footHoldTime = WitchValueManager.I.FootholdContinueTime;
        _spawnTime = 0.0f;
    }
	
	protected override void Update ()
    {
        base.Update();
        //RotateToTarget(_player.transform.position);
        //if (Input.GetMouseButtonDown(0)) _isGravity = true;
        GroggyCheck();
        AnimDelayUpdate();
        FootholdUpdate();
        _attackIdx = Anim.GetInteger("AttackIdx");
        if (_isTel)
        {
            CCameraFind._instance.isLockOn = false;
        }
    }

    protected override void MoveUpdate()
    {
        Vector3 nextPos = transform.position;

        if (_isMoving)
            nextPos = Vector3.MoveTowards(transform.position, _destination, _stat.MoveSpeed * Time.deltaTime);

        Vector3 deltaMove = nextPos - transform.position;

        if (_isGravity)
            deltaMove.y += Physics.gravity.y * Time.deltaTime;

        _controller.Move(deltaMove);
    }

    public void SetState(WitchState state)
    {
        if (_curState == state)
            return;

        _stateSystem.SetState(state);
    }

    public void SetAttack()
    {
        _isAttacking = true;
        _anim.SetInteger("State", (int)WitchState.Attack);
        _anim.SetInteger("AttackIdx", Random.Range(0, 2));
    }

    public override void OnDamage(float damage, float value = 10.0f)
    {
        base.OnDamage(damage, value);
        _receiveDamage += damage;
        _receiveDamage2 += damage;
        StartCoroutine(Co_ChangeMat());
        AddGroggyValue(value);
        OnAnimDelay();
    }

    public void AddGroggyValue(float value)
    {
        if (_curState == WitchState.Groggy ||
            _curState == WitchState.GroggyRelease ||
            _curState == WitchState.AttackRelease)
            return;

        _groggyValue += value;
        _groggyInitTime = 0.0f;
    }

    private void FootholdUpdate()
    {
        if (!_footHoldOn)
            return;

        _footHoldTime += Time.deltaTime;
        _spawnTime += Time.deltaTime;

        if (_footHoldTime >= WitchValueManager.I.FootholdContinueTime &&
            _spawnTime >= WitchValueManager.I.MonsterSpawnTime)
        {
            _spawner.OnSpawn();
            _footHoldTime = 0.0f;
            _spawnTime = 0.0f;
            return;
        }

        if (_footHoldTime >= WitchValueManager.I.FootholdContinueTime)
        {
            _skillSystem.OnPassiveSkill(WitchSkill.C, _player.transform);
            _footHoldTime = 0.0f;
        }

        //else if (_spawnTime >= WitchValueManager.I.MonsterSpawnTime)
        //{
        //    _spawner.OnSpawn();
        //    _spawnTime = 0.0f;
        //}
    }

    public void FootholdOnSkill()
    {
        if (_player == null)
            return;

        _footHoldOn = true;
        //StartCoroutine(Co_OnFootHold());
    }

    public void OffFoothold()
    {
        //StopCoroutine(Co_OnFootHold());
        //_coroutineOn = false;
        _footHoldOn = false;
    }

    private IEnumerator Co_OnFootHold()
    {
        _coroutineOn = true;
        _skillSystem.OnPassiveSkill(WitchSkill.C, _player.transform);
        yield return new WaitForSeconds(WitchValueManager.I.FootholdContinueTime);
        StartCoroutine(Co_OnFootHold());
    }

    public bool DistanceCheck(float distance)
    {
        Vector3 from = transform.position;
        from.y = 0;
        Vector3 to = _player.transform.position;
        to.y = 0;

        if (Vector3.Distance(from, to) <= distance)
            return true;

        return false;
    }

    public void EndAttack()
    {
        _isAttacking = false;
    }

    public void SetAnimation(WitchState state)
    {
        _anim.SetInteger("State", (int)state);
    }

    private IEnumerator Co_ChangeMat()
    {
        _bossMat.GetComponent<Renderer>().material = _damageMat;
        yield return new WaitForSeconds(0.2f);
        _bossMat.GetComponent<Renderer>().material = _oldMat;
    }

    private void GroggyCheck()
    {
        if (_groggyValue < 1)
            return;

        _groggyInitTime += Time.deltaTime;

        if (_groggyValue >= WitchValueManager.I.GroggyMaxValue)
        {
            _groggyValue = 0.0f;
            _groggyInitTime = 0.0f;
            SetState(WitchState.Groggy);
            return;
        }

        if (_groggyInitTime >= WitchValueManager.I.GroggyDeleteTime)
        {
            _groggyValue = 0.0f;
            _groggyInitTime = 0.0f;
        }
    }

    private void OnAnimDelay()
    {
        if (_curState != WitchState.Attack)
            return;

        _animDelayTime = 0.0f;
        _animDelay = true;
        _anim.speed = 0.0f;
    }

    private void AnimDelayUpdate()
    {
        if (!_animDelay)
            return;

        _animDelayTime += Time.deltaTime;

        if (_animDelayTime < WitchValueManager.I.HitAnimStopDuration)
            return;

        _anim.speed = 1.0f;
        _animDelayTime = 0.0f;
        _animDelay = false;
    }
}
