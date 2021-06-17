using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MakerColliderTrigger : MakerMasterTirgger
{
    private BoxCollider coll;

    private void Start()
    {
        Init();
        coll = GetComponent<BoxCollider>();
        coll.isTrigger = true;
        coll.enabled = false;
        if (coll == null) Debug.Log($"Coll was null", this);
    }

    private void Update()
    {
        //Debug.Log(isActiv, this);
        if (coll.enabled == false && isActiv)
        {
            coll.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        maker.DestroyMarker();
        Destroy(this.gameObject);
    }
}
