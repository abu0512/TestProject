using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterUI : MonoBehaviour
{
    private CPlayerMove _CPlayerMove = null;
    public CPlayerMove _PlayerMove { get { return _CPlayerMove; } }

    public float saveHP { get; set; }
    public float maxHP { get; set; }
    public float curHP { get; set; }
    public float maxSP { get; set; }
    public float curSP { get; set; }


    void Start()
    {
        InitParams();
    }

    public virtual void InitParams()
    {
        
    }
}

