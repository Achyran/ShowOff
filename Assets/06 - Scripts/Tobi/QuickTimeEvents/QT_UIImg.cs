using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class QT_UIImg : MonoBehaviour
{
    private RawImage img;
    void Start()
    {
        img = GetComponent<RawImage>();
        img.enabled = false;
        if (QuickTimeMaster.current != null)
        {
            QuickTimeMaster.current.onQuickTimeStart += StartDisplay;
            QuickTimeMaster.current.onQuickTimeEnd += EndDisplay;
        }
        else
        {
            Debug.Log($"Quick time master was null, deleating this", this);
            Destroy(this);
        }

    }

    private void EndDisplay(QuickTimeComponent arg1, bool arg2)
    {
        img.enabled = false;
    }

    private void StartDisplay(QuickTimeComponent obj)
    {
        if (obj._event.UItexture != null)
        {
            img.enabled = true;
            img.texture = obj._event.UItexture;
        }
    }
    private void OnDestroy()
    {
        if (QuickTimeMaster.current != null)
        {
            QuickTimeMaster.current.onQuickTimeStart -= StartDisplay;
            QuickTimeMaster.current.onQuickTimeEnd -= EndDisplay;
        }
    }


}
