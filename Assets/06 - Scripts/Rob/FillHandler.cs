using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FillHandler : MonoBehaviour
{
    Image image;
    [SerializeField]
    float posessionTime;

    float MaxposessionTime;

    [SerializeField]
    bool isActive = false;

    //bool isResetting = false;
    [SerializeField]
    Color fillColor;

    [SerializeField]
    float resetTime = 20;

    [SerializeField]
    Material colorgradient;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        MaxposessionTime = posessionTime;
        colorgradient.SetFloat("gradient", posessionTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        


        if (isActive)
        {
            image.fillAmount = posessionTime / MaxposessionTime;
            posessionTime = posessionTime - Time.deltaTime;
            if (posessionTime <= 0)
            {
                //Debug.Log("Time is up");
                isActive = false;
                //isResetting = true;
            }
        } else if (!isActive)
        {
            
            image.fillAmount = 1;
            posessionTime = MaxposessionTime;

        }

              colorgradient.SetFloat("gradient", image.fillAmount);


        //else if (isResetting)
        //{
        //    resetTime = resetTime - Time.deltaTime;
        //    if (resetTime <= 0)
        //    {
        //        Debug.Log("Time is up");
        //    }

        //}





    }

    //For acsess outside the calss -Tobi
    public void SetAndStart(float possTime)
    {
        MaxposessionTime = possTime ;
        posessionTime = possTime ;
        isActive = true;
    }
    public void ResetDrain()
    {
        isActive = false;
    }
}
