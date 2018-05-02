using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MushroomAnimatorEvent : MonoBehaviour
{
    private GuardMushroom _GuardMushroom;
    private QueenMushroom _QueenMushroom;

    private void Awake()
    {
        _GuardMushroom = transform.GetComponent<GuardMushroom>();
        _QueenMushroom = transform.GetComponent<QueenMushroom>();
    }

    void GuardMHitCheck()
    {
        GuardMushroomAttack _GuardMAttaked = _GuardMushroom.GetCurrentState() as GuardMushroomAttack;
        if (_GuardMAttaked != null)
        {
            _GuardMAttaked.AttackCheck();
        }
    }

    void GuardBMHitCheck()
    {
        GuardMushroomBAttack _GuardBMAttaked = _GuardMushroom.GetCurrentState() as GuardMushroomBAttack;
        if (_GuardBMAttaked != null)
        {
            _GuardBMAttaked.BAttackCheck();
        }
    }

    void QueenHitCheck()
    {
        QueenMushroomAttack _QueenMAttaked = _QueenMushroom.GetCurrentState() as QueenMushroomAttack;
        if (_QueenMAttaked != null)
        {
            Debug.Log("Not null");
        }
    }

    void QueenHealCheck()
    {
        QueenMushroomHealing _QueenMHeal = _QueenMushroom.GetCurrentState() as QueenMushroomHealing;
        if (_QueenMHeal != null)
        {
            _QueenMHeal.HealCheck();
        }
    }
}
