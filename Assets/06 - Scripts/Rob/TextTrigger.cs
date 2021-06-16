using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField]
    CanvasGroup canvasgroup;

    
    GameObject trigger;

    // Start is called before the first frame update
    void Start()
    {
       
        canvasgroup.alpha = 0;

        trigger = gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        canvasgroup.alpha = 1;
    }

    private void OnTriggerExit(Collider other)
    {
        canvasgroup.alpha = 0;
    }
}
