using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{

    [SerializeField]
    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        canvas.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        this.enabled = false;
    }
}
