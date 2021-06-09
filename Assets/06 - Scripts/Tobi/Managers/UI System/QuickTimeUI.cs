using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuickTimeUI : MonoBehaviour
{
    private TextMeshProUGUI text;
    private RawImage img;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        img = GetComponentInChildren<RawImage>();
        if(text == null)
        {
            Debug.Log("Cound not find Text");
        }
        if(img == null)
        {
            Debug.Log("Could not find IMG");
        }
        

    }

    private void SetTextAndIMG()
    {

    }
    private void removeTextAndIMG()
    {

    }
}
