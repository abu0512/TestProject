using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMoveCheack
{
    Grass,
    Rock
}
public class CPlayerMoveSound : CPlayerBase
{
    public PlayerMoveCheack _PlayerMoveCheack = PlayerMoveCheack.Grass;


    private void Start()
    {
    }
    private void Update()
    {        
    }
    public void SoundType(int type)
    {
        SoundManager.I.SoundPlay(GetComponent<Transform>(), type);
    }
    public void MoveSoundPlay()
    {
        if (_PlayerMoveCheack == PlayerMoveCheack.Rock)
        {
            if (_PlayerManager._PlayerSwap._PlayerMode == PlayerMode.Shield)
                SoundManager.I.SoundPlay(GetComponent<Transform>(),  1);
            else
                SoundManager.I.SoundPlay(GetComponent<Transform>(),  3);
        }
        else if (_PlayerMoveCheack == PlayerMoveCheack.Grass)
        {
            if (_PlayerManager._PlayerSwap._PlayerMode == PlayerMode.Shield)
                SoundManager.I.SoundPlay(GetComponent<Transform>(),  0);
            else
                SoundManager.I.SoundPlay(GetComponent<Transform>(),  2);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Rock")
        {
            _PlayerMoveCheack = PlayerMoveCheack.Rock;
        }
        if (other.gameObject.tag == "Grass")
        {
            _PlayerMoveCheack = PlayerMoveCheack.Grass;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _PlayerMoveCheack = PlayerMoveCheack.Grass;
    }
}
