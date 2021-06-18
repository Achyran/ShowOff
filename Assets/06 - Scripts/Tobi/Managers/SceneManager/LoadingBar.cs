using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadingBar : MonoBehaviour
{
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"FillAmount {Loader.LoadingProgres()}",this);
        img.fillAmount = Loader.LoadingProgres();
    }
}
