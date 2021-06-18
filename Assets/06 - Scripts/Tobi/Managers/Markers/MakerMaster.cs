using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(GameMaster))]
public class MakerMaster : Master
{
    public static MakerMaster current;
    public override void Init()
    { 
        //Inicialising the static reference
        if (current == null)
        {
            current = this;
        }
        else if (current != this)
        {
            if (GameMaster.current.debug) Debug.LogWarning("Multible GameMasters detected, Destroying this", this);
            Destroy(this);
        }
    }

    public override void ScenneStart()
    {
    }

    public event Action<MarkerComponent> onActivate;
    public void Activate(MarkerComponent markerComponent)
    {
        if(onActivate != null)
        {
            onActivate(markerComponent);
        }
    }
}
