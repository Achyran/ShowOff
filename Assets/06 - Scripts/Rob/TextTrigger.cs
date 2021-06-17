using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField]
    CanvasGroup canvasgroup;

    public bool destroyafteruse;

    [SerializeField]
    GameObject target;

    
    

    // Start is called before the first frame update
    void Start()
    {
       
        canvasgroup.alpha = 0;

        GameMaster.current.onInspectionStart += DisableText;

       
    }

    private void DisableText(GameObject obj)
    {
        if(obj != null && obj == target)
        {
            canvasgroup.alpha = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canvasgroup.alpha = 1;
    }

    private void OnTriggerExit(Collider other)
    {
        canvasgroup.alpha = 0;
        if (destroyafteruse)
        {

            Destroy(this.gameObject);
        }
    }

    private void OnDisable()
    {
        GameMaster.current.onInspectionStart -= DisableText;
    }
}
