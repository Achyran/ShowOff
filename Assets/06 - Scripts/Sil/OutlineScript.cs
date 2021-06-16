
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OutlineScript : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] public float highlitScale;
    [SerializeField] public float nonHighlitScale;
    [SerializeField] private Color outlineColor;
    private float outlineScaleFactor;
    private Renderer outlineRenderer;
    private GameObject outlineObject;

    // Start is called before the first frame update
    void Start()
    {
        outlineScaleFactor = nonHighlitScale;
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        outlineRenderer.enabled = true;

    }

    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color) 
    {
        outlineObject = Instantiate(gameObject, transform.position, transform.rotation, transform);
        Renderer rend = outlineObject.GetComponent<Renderer>();

        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_Scale", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObject.GetComponent<OutlineScript>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;

        //rend.enabled = false;

        ToggleOutline(false);
        return rend;
    }

    public void ToggleOutline(bool val)
    {
        if (val == true)
            outlineScaleFactor = highlitScale;
        else
            outlineScaleFactor = nonHighlitScale;

        outlineRenderer.material.SetFloat("_Scale", outlineScaleFactor);
    }
}
