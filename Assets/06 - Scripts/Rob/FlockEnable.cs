using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockEnable : MonoBehaviour
{
    GameObject FlockMaster;


    // Start is called before the first frame update
    void Start()
    {
        FlockMaster = GameObject.FindGameObjectWithTag("FlockMaster").gameObject;

        Debug.Log(FlockMaster);
        FlockMaster.SetActive(false);
    }

    // Update is called once per frame
  

    private void OnTriggerEnter(Collider other)
    {
        if (FlockMaster != null)
        {
            FlockMaster.SetActive(true);
            Debug.Log("if");

        } else
        {
            Debug.Log("Flockmaster is null");
        }
    }
}
