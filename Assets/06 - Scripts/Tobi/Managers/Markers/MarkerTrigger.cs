using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerTrigger : MonoBehaviour
{

    [SerializeField]
    public GameObject newMarker;

    [SerializeField]
    public GameObject oldMarker;

    // Start is called before the first frame update
    void Start()
    {
        if(newMarker != null) { 

        newMarker.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(newMarker != null)
        {

            newMarker.SetActive(true);
        }
        

        if(oldMarker != null)
        {
            oldMarker.SetActive(false);
        }
        
    }

    private void Update()
    {
    
    }
}
