using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//using System;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CamConnection : MonoBehaviour
{

    public bool isStartCam;
    public GameObject target;
    public CinemachineFreeLook freeLook { get; private set; }

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
        else {
            Debug.LogError("The ConectionMaset was null, Destroying this object", this);
            Destroy(gameObject);
        }
        freeLook = GetComponent<CinemachineFreeLook>();
        if (isStartCam)
        {
            if (CamMaster.current.currentConnectionIndex != -1)
            {
                Debug.LogWarning($"A different Cam is allready active. Active Cam = {CamMaster.current.connections[CamMaster.current.currentConnectionIndex]}", this);
                freeLook.enabled = false;
            }else
            {
                CamMaster.current.SetCam(target);
            }
        }
        else freeLook.enabled = false;
        freeLook.LookAt = target.transform;
        freeLook.Follow = target.transform;
    }
    

    private void UpdateCam(CamConnection obj)
    {
        if(obj.freeLook == freeLook)
        {
            freeLook.enabled = true;
        }else
        {
            freeLook.enabled = false;
        }
    }

    private void OnDestroy()
    {
        CamMaster.current.onConnectionUpdate -= UpdateCam;
    }
}
