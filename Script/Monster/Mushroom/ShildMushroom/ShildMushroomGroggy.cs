using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildMushroomGroggy : ShildMushroomStateBase
{
    Vector3 SavePosition;
    ShildMushroomEffect _groggy;

    public override void BeginState()
    {
        base.BeginState();
        _groggy = GetComponent<ShildMushroomEffect>();
        Dltime = 0f;
        SavePosition = transform.position;
    }

    public override void EndState()
    {
        base.EndState();
    }


    void Update()
    {
        _groggy.Groggy(transform.position);
        Dltime += Time.deltaTime;
        ShildMushroom.GoToDestination(SavePosition, 0, 0);

        if (Dltime > 5f)
        {
            ShildMushroom.SetState(ShildMushroomState.Return);
            _groggy.GroggyEffect.SetActive(false);
            return;
        }
    }
}
