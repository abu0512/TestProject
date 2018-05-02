using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMaanger : MonoBehaviour
{
    public GameObject BossParams;
    public GameObject BossCharacter;

    void BossParamsSet()
    {
        if(BossCharacter.activeInHierarchy)
        {
            BossParams.SetActive(true);
        }
    }

	void Awake ()
    {
		
	}
	
	void Update ()
    {
        BossParamsSet();
    }
}
