using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager I = null;

    [FMODUnity.EventRef]
    public string[] MyEvent1;

    FMOD.Studio.EventInstance bgmSound;
    FMOD.Studio.ParameterInstance bgmVolume;

    public float _Volume;

    void Awake()
    {
        SoundManager.I = this;
        _Volume = 1;
    }
    void Help()
    {
        /* 
                    타겟위치,  중력,  사운드번호
        SoundPlay(Transform Target, Rigidbody rig, int SoundType)
        SoundType = 0 -> 검방베리 풀걷기
        SoundType = 1 -> 검방베리 돌걷기
        SoundType = 2 -> 흑화베리 풀걷기
        SoundType = 3 -> 흑화베리 돌걷기
        SoundType = 4 -> 검방베리 타격
        SoundType = 5 -> 검방베리 방패막기
        SoundType = 6 -> 검방베리 반격
        SoundType = 7 -> 흑화베리 타격
        SoundType = 8 -> 흑화베리 광역
        */
    }
    public void Update()
    {

        if (!Input.GetKey(KeyCode.A) &&
        !Input.GetKey(KeyCode.D) &&
        !Input.GetKey(KeyCode.W) &&
        !Input.GetKey(KeyCode.S))
        {
            _Volume = 1;
        }
        else
            _Volume = 0;

        bgmVolume.setValue(_Volume);
    }


    public void SoundPlay(Transform Target, int SoundType)
    {
        bgmSound = FMODUnity.RuntimeManager.CreateInstance(MyEvent1[SoundType]);
        bgmSound.getParameter("Parameter 1", out bgmVolume);
        //FMODUnity.RuntimeManager.PlayOneShot(MyEvent1[SoundType], Target.position);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(bgmSound, Target, GetComponent<Rigidbody>());
        bgmSound.start();
    }
}