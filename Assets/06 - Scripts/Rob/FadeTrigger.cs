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

    [SerializeField]
    private bool locked = false;

    private void Start()
    {
       
        FadeOutAnimation = GameObject.FindGameObjectWithTag("SceneFadeOut").GetComponent<Animation>();
        FadeInAnimation = GameObject.FindGameObjectWithTag("SceneFadeIn").GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!locked)
        {
            if (Fadeout) FadeOutAnimation.Play();
            if (Fadein) FadeInAnimation.Play();
        }
    }
    public void Unlock()
    {
        locked = false;
    }

}
