using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//using System;

public class CamConnection : MonoBehaviour
{

    public bool isStartCam;
    
    public GameObject target;

    [Tooltip("Set this to true if you want to manually set target and follow (for if they are different)")]
    public bool ignoreTarget = false;
    private bool setStart = false;
    public CinemachineVirtualCameraBase virtualCam { get; private set; }


    //This needs some work
    private void Start()
    {
        
        if (target == null)
        {
            Debug.LogError("The Conection Target was not set, Destroying this object", this);
            Destroy(gameObject);
        }

        if (CamMaster.current != null)
        {
            CamMaster.current.onConnectionUpdate += UpdateCam;
        }
        else
        {
            Debug.LogError("The ConectionMaset was null, Destroying this object", this);
            Destroy(gameObject);
        }

        if (GetComponent<CinemachineFreeLook>() != null)
            virtualCam = GetComponent<CinemachineFreeLook>();
        else
            virtualCam = GetComponent<CinemachineVirtualCamera>();
       


        if (!ignoreTarget)
        {
            virtualCam.LookAt = target.transform;
            virtualCam.Follow = target.transform;
        }

        virtualCam.enabled = false;
    }
    private void Update()
    {
        if (setStart)
        {
            virtualCam.enabled = true;
            setStart = false;
        }
    }


    public void EnableVirtualCam()
    {
        //Debug.Log("Called enabled");
        setStart = true;
    }

    private void UpdateCam(CamConnection obj)
    {
        //Debug.Log($"Update Cam {obj.virtualCam == virtualCam}",this);
        if(obj.virtualCam == virtualCam)
        {
            virtualCam.enabled = true;
        }else
        {
            virtualCam.enabled = false;
        }
    }

    private void OnDestroy()
    {
        CamMaster.current.onConnectionUpdate -= UpdateCam;
    }
}
