using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBlock : ProgressionBlock
{

    private void Start()
    {
        if(ProgressionMaster.current != null)
        {
            ProgressionMaster.current.onTriggered += Listen;
        }
    }

    private void Listen(ProgressionBlock obj)
    {
        if (obj == this) Unlock();
    }

    public override void Unlock()
    {
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        if (ProgressionMaster.current != null)
        {
            ProgressionMaster.current.onTriggered -= Listen;
        }
    }
}
