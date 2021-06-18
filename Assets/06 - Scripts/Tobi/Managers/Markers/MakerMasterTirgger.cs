using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakerMasterTirgger : MonoBehaviour
{

    public MarkerComponent maker;
    public bool isActiv { private set; get; }

    public void Init()
    {
        if (maker.isActiveAtStart) isActiv = true;
        if (MakerMaster.current != null) MakerMaster.current.onActivate += Activate;
    }

    private void Activate(MarkerComponent obj)
    {
        if(obj == maker)
        {
            isActiv = true;
        }
    }

    private void OnDestroy()
    {
        if (MakerMaster.current != null) MakerMaster.current.onActivate -= Activate;
    }

}
