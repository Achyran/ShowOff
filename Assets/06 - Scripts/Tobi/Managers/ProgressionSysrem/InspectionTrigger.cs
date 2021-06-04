using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionTrigger : ProgressionTrigger
{
    [SerializeField]
    private ProgressionBlock block;
    [SerializeField]
    private GameObject inspection;

   

    // Start is called before the first frame update
    void Start()
    {
        if(GameMaster.current != null)
        {
            GameMaster.current.onInpsectionStop += Listen;
        }
        
    }

    private void Listen(GameObject obj)
    {
        if (obj == inspection) Triggerd(block);
    }
}
