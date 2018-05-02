using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerLockOnRot : MonoBehaviour
{
    public static CPlayerLockOnRot _instance = null;

    private void Start()
    {
        CPlayerLockOnRot._instance = this;
    }

    public void LockRot(Transform target)
    {
        Vector3 curPos = target.position - transform.position;
        curPos.Normalize();
        
        Quaternion q = transform.rotation;
        q.x = 0.0f;
        
        if (Vector3.Distance(transform.position, target.position) < 2.0f)
        {
            transform.rotation = q;
        }
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(curPos), Time.deltaTime * 20f);

    }

}
