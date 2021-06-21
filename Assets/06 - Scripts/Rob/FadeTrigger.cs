using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class FadeTrigger : MonoBehaviour
{


   
    
    Animation FadeOutAnimation;
    Animation FadeInAnimation;

    public bool Fadeout;
    public bool Fadein;

    private void Start()
    {
       
        FadeOutAnimation = GameObject.FindGameObjectWithTag("SceneFadeOut").GetComponent<Animation>();
        FadeInAnimation = GameObject.FindGameObjectWithTag("SceneFadeIn").GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
         if(Fadeout)FadeOutAnimation.Play();
         if(Fadein)FadeInAnimation.Play(); 
       
    }

}
