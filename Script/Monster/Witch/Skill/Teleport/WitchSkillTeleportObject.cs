using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkillTeleportObject : MonoBehaviour
{
    private WitchBoss _witch;
    private WitchSkillBase _skill;
    private Transform _target;
    private int _state = 0;
    private GameObject _in;
    private GameObject _out;
    private float _teleportTime;
    private Vector3 _destination;
    private int _type;
    private float _witchY;
    private bool _init;

    public void Init(WitchSkillBase skill)
    {
        _witch = skill.Witch;
        _skill = skill;
        _state = 0;
        _in = transform.Find("In").gameObject;
        _out = transform.Find("Out").gameObject;

        _in.SetActive(false);
        _out.SetActive(false);
    }

    void Update()
    {
        switch (_state)
        {
            case 1:
                TeleportUpdate();
                break;
        }
    }

    public void OnSkill(Transform target, int type = 0)
    {
        if (_skill.IsOn)
            return;

        _witch.IsTel = true;

        _witchY = _witch.transform.position.y;
        _skill.IsOn = true;
        _state = 1;
        _skill.IsOn = true;
        _target = target;
        _teleportTime = 0.0f;
        _type = type;
        _init = false;

        _in.SetActive(true);
        Vector3 telPos = _witch.transform.position;
        telPos.y = _in.transform.position.y;
        _in.transform.position = telPos;
        _out.transform.position = telPos;
        _out.SetActive(false);

        if (_type == 0)
            _destination = ((_witch.transform.forward * -1) * 10.0f) + _witch.transform.position;
        else if (_type == 2)
        {
            _destination = Vector3.zero;
            _destination.y = _witch.transform.position.y;
        }
    }

    private void TeleportUpdate()
    {
        _teleportTime += Time.deltaTime;

        if (_teleportTime < 0.1f)
            return;

        _witch.transform.Translate(Vector3.down * 100.0f);

        if (_teleportTime < 1.0f)
            return;

        if (_type == 1 &&
            !_init)
        {
            _destination = _witch.Target.transform.position + (GetRandomDirection() * 
                (_witch.Stat.AttackDistance - 1.0f));
            _destination.y = _witchY;
            _init = true;
        }

        _in.SetActive(false);
        _out.SetActive(true);
        Vector3 telPos = _destination;
        telPos.y = _out.transform.position.y;
        _out.transform.position = telPos;

        if (_teleportTime < 1.2f)
            return;

        _witch.transform.position = _destination;
        Vector3 target = _witch.Target.transform.position;
        target.y = _witch.transform.position.y;
        _witch.RotateToTarget(target);
        _skill.IsOn = false;
        _witch.IsTel = false;
        _state = 2;
    }

    private Vector3 GetRandomDirection()
    {
        Vector3 randDir = Vector3.zero;

        while (true)
        {
            int x = Random.Range(-1, 2);
            int z = Random.Range(-1, 2);

            if (x == 0 &&
                z == 0)
                continue;

            randDir = new Vector3(x, 0.0f, z);
            break;
        }

        return randDir;
    }
}
