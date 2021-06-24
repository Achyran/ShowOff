using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerTriggerPosession : MakerMasterTirgger
{
    [SerializeField]
    PosessionMovement target;

    Renderer target_renderer;
    // Start is called before the first frame update
    void Start()
    {

        Init();
        if (target == null)
        {
            Debug.LogWarning($"Inpsection Target was null, pleas set it in the inspector", this);
        }
        if (GameMaster.current != null) GameMaster.current.onPosessionStart += Triggerd;
    }

    private void Triggerd(PosessionMovement obj)
    {
        if (obj == target)
        {
            maker.DestroyMarker();
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (GameMaster.current != null) GameMaster.current.onPosessionStart -= Triggerd;
    }
}
