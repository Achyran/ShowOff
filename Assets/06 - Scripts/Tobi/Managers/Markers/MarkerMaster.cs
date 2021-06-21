using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(GameMaster))]
public class MarkerMaster : Master
{
    public static MarkerMaster current;


    public Compass compass;
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
        
        /*
        compass = FindObjectOfType<Compass>();
        if(compass == null)
            Debug.Log("No compass found");
        */
    }


    public override void ScenneStart()
    {
        /*
        compass = FindObjectOfType<Compass>();
        if (compass = null)
            Debug.LogError("No compass found in scene");
        */
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
