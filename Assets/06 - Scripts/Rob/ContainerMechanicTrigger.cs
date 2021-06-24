using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerMechanicTrigger : MonoBehaviour
{
    
    
    ContainerController controller;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("ContainerController").GetComponent<ContainerController>();
        controller.enabled = false;

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            controller.enabled = true;
        }
    }
}
