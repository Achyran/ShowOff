using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverLay : MonoBehaviour
{
    [SerializeField]
    private float maxAlpha;
    [SerializeField]
    private float sensitivity;
    private RawImage img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<RawImage>();
        if (img == null) Debug.LogError($"Image is Missing  on {this}"); 
    }

    // Update is called once per frame
    void Update()
    {
        if(img != null && img.color.a <= maxAlpha)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, EventManager.current.trashAmount * sensitivity);
        }
    }
}
