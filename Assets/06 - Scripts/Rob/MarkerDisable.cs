using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerDisable : MonoBehaviour
{

    [SerializeField]
    GameObject target;

    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
       GameMaster.current.onInspectionStart += DisableMarker;
    }
    
    void DisableMarker(GameObject obj)
    {
        if(obj == target)
        {
            renderer.enabled = false;
            Debug.Log("marker is disabled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameMaster.current.onInspectionStart -= DisableMarker;
    }
}
