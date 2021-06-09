using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FillHandler))]
public class PossesionUIDrain : MonoBehaviour
{
    private FillHandler fill;

    // Start is called before the first frame update
    void Start()
    {
        fill = GetComponent<FillHandler>();
        if(GameMaster.current == null)
        {
            Debug.LogError("Game master was null, destying this", this);
            Destroy(this);
        }
        GameMaster.current.onPosessionStart += StartDrain;
        GameMaster.current.onPosessionStop += StopDrain;
    }

    private void StopDrain()
    {
        fill.ResetDrain();
    }

    private void StartDrain(PosessionMovement obj)
    {
        fill.SetAndStart(obj.posessionTime);
    }

    private void OnDestroy()
    {
        GameMaster.current.onPosessionStart -= StartDrain;
        GameMaster.current.onPosessionStop -= StopDrain;
    }
}
