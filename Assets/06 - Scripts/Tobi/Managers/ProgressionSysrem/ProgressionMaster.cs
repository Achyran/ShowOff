using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(GameMaster))]
public class ProgressionMaster : Master
{
    public static ProgressionMaster current;

    public override void Init()
    {
        if(current == null)
        {
            current = this;
        }else
        {
            Debug.LogWarning("Mulitbel ProgressionMaster were detected, Destroying this", this);
            Destroy(this);
        }
    }


    public event Action<ProgressionBlock> onTriggered;
    public void Triggered(ProgressionBlock block)
    {
       if(onTriggered != null)
       {
            onTriggered(block);
       }
    }
}
