using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerMasterQTTrigger : MakerMasterTirgger
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Triggered()
    {
        Debug.Log("Calling Triggerd");
            maker.DestroyMarker();
            Destroy(this);
    }
}
