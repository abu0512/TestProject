using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomSbombing : GuardMushroomStateBase
{
    public ParticleSystem SbombEffect;

    public override void BeginState()
    {
        base.BeginState();
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void EffectofSbomb(Vector3 From)
    {
        From.y += 1f;
        transform.position = From;
        Instantiate(SbombEffect, transform.position, Quaternion.identity);
    }

    void Update()
    {
        GuardMushroom.GoToPullPush();
        GuardMushroom.GoToDestination(GuardMushroom.Player.position, GuardMushroom.BerserkerMoveSpeed, GuardMushroom.rotAnglePerSecond);

        if (GuardMushroom.GetDistanceFromPlayer() < 1f)
        {
            EffectofSbomb(transform.position);
            GuardMushroom.OnDead();
        }

        if (GuardMushroom.SbombTimer > 6f)
        {
            EffectofSbomb(transform.position);
            GuardMushroom.OnDead();
        }

    }
}
