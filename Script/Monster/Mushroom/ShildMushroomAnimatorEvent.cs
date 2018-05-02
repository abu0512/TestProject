using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildMushroomAnimatorEvent : MonoBehaviour {

    private ShildMushroom _ShildMushroom;

    private void Awake()
    {
        _ShildMushroom = transform.GetComponent<ShildMushroom>();
    }

    void ShildMHitCheck()
    {
        ShildMushroomAttack _ShildMAttaked = _ShildMushroom.GetCurrentState() as ShildMushroomAttack;
        if (_ShildMAttaked != null)
        {
            _ShildMAttaked.ShildAttackCheck();
        }
    }
}
