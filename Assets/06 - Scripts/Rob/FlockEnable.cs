using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

public class FlockEnable : MonoBehaviour
{
    [SerializeField]
    PlayableDirector FlockMaster;


    // Start is called before the first frame update
    void Start()
    {


        FlockMaster = GameObject.FindGameObjectWithTag("FlockMaster").GetComponent<PlayableDirector>();

        if (FlockMaster != null)
        {
            FlockMaster.enabled = false;
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
            FlockMaster.enabled = true;


        }
        else if (FlockMaster == null) 
        {
            Debug.Log("Flockmaster is null");
        }
    }
}
