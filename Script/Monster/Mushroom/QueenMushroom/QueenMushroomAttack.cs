using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QueenMushroomAttack : QueenMushroomStateBase
{
    private float RandomNum;

    public override void BeginState()
    {
        Dltime = 0f;
        RandomNum = Random.Range(0, 9f);
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void StartAttack()
    {
        QueenMushroom.AttackStack++;
        if (QueenMushroom.AttackStack > 2f && RandomNum > 2f)
        {
            Stage1.I.BulletPool.SetStunBullet(_QueenMushroom, transform.position, QueenMushroom.Player.position);
            QueenMushroom.AttackStack = 0;
        }

        else
        {
            Stage1.I.BulletPool.SetBullet(_QueenMushroom, transform.position, QueenMushroom.Player.position);
        }

        QueenMushroom.AttackTimer = 0f;
        Dltime = 0f;
    }

    void Update()
    {
        Dltime += Time.deltaTime;

        QueenMushroom.GoToPullPush();
        QueenMushroom.PlayerisDead();
        QueenMushroom.TurnToDestination();
        QueenMushroom.TimeToHeal();

        if (Dltime > 1.5f)
        {
            if (QueenMushroom.GetDistanceFromPlayer() > QueenMushroom.MStat.AttackDistance)
            {
                QueenMushroom.SetState(QueenMushroomState.Chase);
                Dltime = 0;
                return;
            }

            else
            {
                QueenMushroom.SetState(QueenMushroomState.Return);
                Dltime = 0;
                return;
            }
        }
    }
}