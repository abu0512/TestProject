using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchStateAttackRelease : WitchFSMStateBase
{
    private GameObject _release;

    public override void BeginState()
    {
        _release = GameObject.Find("WitchSkillRelease");
        _release.SetActive(false);
    }

    void Update()
    {
    }

    public override void EndState()
    {
    }

    public void OnRelease()
    {
        Vector3 pos = Witch.transform.position;
        _release.transform.position = pos;
        _release.SetActive(true);
    }

    public void EndReleaseAnim()
    {
        Witch.SetState(WitchState.Chase);
        return;
    }
}
