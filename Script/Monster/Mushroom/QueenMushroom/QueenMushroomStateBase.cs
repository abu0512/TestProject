﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(QueenMushroom))]
public class QueenMushroomStateBase : MonoBehaviour
{
    public float Dltime;
    protected QueenMushroom _QueenMushroom;
    protected QueenMushroom QueenMushroom { get { return _QueenMushroom; } set { _QueenMushroom = value; } }

    private void Awake()
    {
        _QueenMushroom = GetComponent<QueenMushroom>();
    }

    public virtual void BeginState()
    {
    }

    public virtual void EndState()
    {
    }
}
