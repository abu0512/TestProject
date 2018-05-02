using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMushroomEffect : MonoBehaviour
{
    public GameObject BerserkerEffect;
    public GameObject[] ShildHitEffects;
    public GameObject[] ScytheHitEffects;
    public GameObject EffectPosition;
    public GameObject PCSwapEffect;


    public float[] ShildHitTime;
    public float[] ScytheHitTime;
    public float SwapTime;

    private Vector3 _home;

    public void GuardSwapEffect()
    {
        _home.y += 2.3f;
        PCSwapEffect.transform.position = _home;
        PCSwapEffect.SetActive(true);
    }

    public void GuardSwapCheck()
    {
        if (PCSwapEffect.activeInHierarchy)
            SwapTime += Time.deltaTime;

        if (SwapTime > 0.7f)
        {
            PCSwapEffect.SetActive(false);
            SwapTime = 0;
        }
    }

    public void GuardMHitEffect()
    {
        if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Scythe)
        {
            _home.y += 2.3f;

            for (int i = 0; i < 3; i++)
            {
                ScytheHitEffects[i].transform.position = _home;
            }

            if (CPlayerManager._instance.m_nAttackCombo == 1)
            {
                ScytheHitEffects[0].SetActive(true);
            }
            else if (CPlayerManager._instance.m_nAttackCombo == 2)
            {
                ScytheHitEffects[1].SetActive(true);
            }
            else if (CPlayerManager._instance.m_nAttackCombo == 3)
            {
                ScytheHitEffects[2].SetActive(true);
            }
        }

        if (CPlayerManager._instance._PlayerSwap._PlayerMode == PlayerMode.Shield)
        {
            _home.y -= 1.3f;

            for (int i = 0; i < 3; i++)
            {
                ShildHitEffects[i].transform.position = EffectPosition.transform.position;
            }

            if (CPlayerManager._instance.m_nAttackCombo == 1)
            {
                ShildHitEffects[0].SetActive(true);
            }
            else if (CPlayerManager._instance.m_nAttackCombo == 2)
            {
                ShildHitEffects[1].SetActive(true);
            }
            else if (CPlayerManager._instance.m_nAttackCombo == 3)
            {
                ShildHitEffects[2].SetActive(true);
            }
        }
    }
    public void BerserkerModeEffect(Vector3 From)
    {
        From.y += 1.35f;
        BerserkerEffect.transform.position = From;
        BerserkerEffect.SetActive(true);
    }

    public void SetHitEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            if (ShildHitEffects[i].activeInHierarchy)
            {
                ShildHitTime[i] += Time.deltaTime;

                if (ShildHitTime[i] > 0.5f)
                {
                    ShildHitEffects[i].SetActive(false);
                    ShildHitTime[i] = 0;
                }
            }

            if (ScytheHitEffects[i].activeInHierarchy)
            {
                ScytheHitTime[i] += Time.deltaTime;

                if (ScytheHitTime[i] > 0.5f)
                {
                    ScytheHitEffects[i].SetActive(false);
                    ScytheHitTime[i] = 0;
                }
            }
        }
    }

    void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            ShildHitTime[i] = 0;
            ScytheHitTime[i] = 0;
        }

        SwapTime = 0;
    }
    void Update()
    {
        _home = transform.position;
        SetHitEffect();
        GuardSwapCheck();
    }
}
