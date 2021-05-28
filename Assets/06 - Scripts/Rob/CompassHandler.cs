using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassHandler : MonoBehaviour
{
    private float trueNorth;

    [SerializeField]
    private GameObject marker;
    private float markerX;
    

    // Start is called before the first frame update
    void Start()
    {
        marker = new GameObject();
        markerX = marker.GetComponent<Transform>().transform.position.x;
        Debug.Log(markerX);

    }

    // Update is called once per frame
    void Update()
    {
        trueNorth = gameObject.transform.rotation.y;
        Debug.Log("TrueNorth = " + trueNorth);

        CompassMarker();
    }

    void CompassMarker()
    {
        markerX = markerX * 100;
        marker.transform.position = new Vector3(markerX, 0, 0); 

    }

}
