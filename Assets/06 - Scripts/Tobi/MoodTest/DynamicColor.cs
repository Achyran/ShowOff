using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicColor : MonoBehaviour
{
    
    private Material material;
    [SerializeField]
    private Color targetColor;
    [SerializeField]
    [Header("The Senestivity in decemal Pecent ")]
    private float senesetivity;
    // Start is called before the first frame update
    void Start()
    {
        //Instanciate the Material
        material = GetComponent<Renderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
       //Calculate the percent of the color
        float colorpercent = EventManager.current.trashAmount *senesetivity;
        colorpercent = Mathf.Clamp(colorpercent, 0f, 1f);

        //creat the ne color by reverling the target color, mulitping by the current percent. and reversing again
        Color currentcollor = new Color(1-(colorpercent * ( 1- targetColor.r)) , 1-(colorpercent * (1-targetColor.g)), 1-(colorpercent * (1-targetColor.b)));
        //aplay new color
        material.SetColor("_BaseColor", currentcollor);   
    }
}
