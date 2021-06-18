using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockEnable : MonoBehaviour
{
    [SerializeField]
    GameObject FlockMaster;


    // Start is called before the first frame update
    void Start()
    {


        FlockMaster = GameObject.FindGameObjectWithTag("FlockMaster");

        if (FlockMaster != null)
        {
            FlockMaster.SetActive(false);
        } else
        {
            Debug.Log("Flockmaster is null");
        }




    }

    // Update is called once per frame
  

    private void OnTriggerEnter(Collider other)
    {
        if (FlockMaster != null)
        {
            FlockMaster.SetActive(true);


        }
        else if (FlockMaster == null) 
        {
            Debug.Log("Flockmaster is null");
        }
    }
}
