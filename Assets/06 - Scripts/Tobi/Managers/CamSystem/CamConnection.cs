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
    public CinemachineVirtualCameraBase virtualCam { get; private set; }

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

        if (isStartCam)
        {
            if (CamMaster.current.currentConnectionIndex != -1)
            {
                Debug.LogWarning($"A different Cam is allready active. Active Cam = {CamMaster.current.connections[CamMaster.current.currentConnectionIndex]}", this);
                virtualCam.enabled = false;
            }
            else
            {
                CamMaster.current.SetCam(target);
            }
        }
        else virtualCam.enabled = false;

        if (!ignoreTarget)
        {
            virtualCam.LookAt = target.transform;
            virtualCam.Follow = target.transform;
        }
    }
    

    private void UpdateCam(CamConnection obj)
    {
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
