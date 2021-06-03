using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionTrigger : ProgressionTrigger
{
    [SerializeField]
    private ProgressionBlock block;
    [SerializeField]
    private CamConnection inspection;

   

    // Start is called before the first frame update
    void Start()
    {
        if (CamMaster.current == null)
        {
            Debug.LogWarning("No CamMaster Found, destrying this ", this);
            Destroy(this);
        }
        else {
            CamMaster.current.onConnectionUpdate += Listen; 
        }
        
    }

    private void Listen(CamConnection connection)
    {
        if (connection == inspection) Triggerd(block);
    }
}
