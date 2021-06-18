using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakerMasterQTTrigger : MakerMasterTirgger
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Triggered()
    {
            maker.DestroyMarker();
            Destroy(this);
    }
}
