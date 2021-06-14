using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class SeeCurrent : MonoBehaviour
{
    private Rigidbody[] rbs;
    [SerializeField]
    private float strenght;
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        rbs = FindObjectsOfType<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        foreach(Rigidbody r in rbs)
        {
            if(r.gameObject == other.gameObject)
            {
                r.AddForce(transform.forward * strenght);
            }
        }
    }
}
