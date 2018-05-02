using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBullet : MonoBehaviour
{
    protected QueenMushroom _queenMushroom;
    protected Vector3 _direction;
    [SerializeField]
    protected float _speed;

    private float DeleteTime;

    void Start()
    {
        DeleteTime = 0;
    }

    void Update()
    {
        DeleteTime += Time.deltaTime;
        transform.Translate(_direction * _speed * Time.deltaTime);

        if (DeleteTime >= 4f)
        {
            gameObject.SetActive(false);
            DeleteTime = 0;
        }
    }

    public void InitStunBullet(QueenMushroom queen, Vector3 from, Vector3 target)
    {
        _queenMushroom = queen;
        from.y += 0.6f;
        from.x += 0.4f;
        target.y = from.y;
        transform.position = from;
        _direction = (target - from).normalized;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MapObject" ||
              other.tag == "Player" ||
              other.tag == "Shild")
        {
            if (other.tag == "Player")
            {
                CPlayerSturn._instance.isSturn = true;
                CPlayerManager._instance.PlayerHp(0.2f, 1, _queenMushroom.AttackDamage);
            }
            else if (other.tag == "Shild")
            {
                CPlayerManager._instance.PlayerHp(0.2f, 2, _queenMushroom.AttackDamage);
            }
            gameObject.SetActive(false);
        }
    }
}