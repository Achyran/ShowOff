using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerTriggerInspection : MakerMasterTirgger
{
    [SerializeField]
    private GameObject inspectionTarget;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        if(inspectionTarget == null)
        {
            Debug.LogWarning($"Inpsection Target was null, pleas set it in the inspector", this);
        }
        if (GameMaster.current != null) GameMaster.current.onInspectionStart += Triggerd;
    }

    private void Triggerd(GameObject obj)
    {
        if(obj == inspectionTarget)
        {
            maker.DestroyMarker();
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (GameMaster.current != null) GameMaster.current.onInspectionStart -= Triggerd;
    }
}
