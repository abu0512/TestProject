using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkillFootholdFire : MonoBehaviour
{
    private Vector3 _target;
    private int _state;
    private Vector3 _readyPos;
    private GameObject[] _fires;
    private float _footholdDuration;
    private float _budeulDuration;
    private CapsuleCollider _collider;

    private void Awake()
    {
        _fires = new GameObject[2];
        _fires[0] = transform.Find("Foothold").gameObject;
        _fires[1] = transform.Find("Explosion").gameObject;
        _collider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        switch (_state)
        {
            case 1:
                FootholdUpdate();
                break;
            case 2:
                break;
        }
    }

    public void OnFire(Transform target)
    {
        _collider.enabled = false;
        _state = 1;
        _target = target.position;
        transform.position = _target;
        _footholdDuration = 0.0f;
        _budeulDuration = 0.0f;
        _fires[0].SetActive(true);
        _fires[1].SetActive(false);
    }

    private void FootholdUpdate()
    {
        _footholdDuration += Time.deltaTime;

        if (_footholdDuration < WitchValueManager.I.FootholdDuration)
            return;

        _fires[0].SetActive(false);
        _fires[1].SetActive(true);

        if (_footholdDuration < WitchValueManager.I.FootholdDuration + 0.1f)
            return;

        _collider.enabled = true;

        if (_footholdDuration < WitchValueManager.I.FootholdDuration + 2.1f)
            return;

        _collider.enabled = false;

        if (_footholdDuration < WitchValueManager.I.FootholdDuration + 3.15f)
            return;

        gameObject.SetActive(false);

        _footholdDuration = 0.0f;
        _state = 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CPlayerManager._instance.PlayerHp(0.2f, 1, WitchValueManager.I.FootholdDamage);
        }
    }
}
