using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProgressionTrigger : MonoBehaviour
{
    public void Triggerd(ProgressionBlock block)
    {
        if (ProgressionMaster.current != null)
        {
            ProgressionMaster.current.Triggered(block);
        }
        else
        {
            Debug.LogWarning("Cound not notifie ProgressionMaseter, Progression Maser was null");
        }
    }

}