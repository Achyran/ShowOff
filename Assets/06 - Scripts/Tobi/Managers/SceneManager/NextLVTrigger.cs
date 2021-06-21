using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NextLVTrigger : MonoBehaviour
{
    private Collider coll;
    [SerializeField]
    private LoaderComp loader;

    void Start()
    {
        coll = GetComponent<Collider>();
        if (loader == null) Destroy(this);
        coll.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
         loader.LoadScene();
    }
}
