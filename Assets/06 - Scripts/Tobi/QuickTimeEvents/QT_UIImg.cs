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
        if (QuickTimeMaster.current != null)
        {
            QuickTimeMaster.current.onQuickTimeStart += StartDisplay;
            QuickTimeMaster.current.onQuickTimeEnd += EndDisplay;
        }
        img.enabled = false;
    }

    private void EndDisplay(QuickTimeComponent arg1, bool arg2)
    {
        img.enabled = false;
    }

    private void StartDisplay(QuickTimeComponent obj)
    {
        img.enabled = true;
        img.texture = obj._event.UItexture;

    }
    private void OnDestroy()
    {
        QuickTimeMaster.current.onQuickTimeStart -= StartDisplay;
        QuickTimeMaster.current.onQuickTimeEnd -= EndDisplay;
    }


}
