using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    AnimationClip FadeIn, FadeOut;
    Animation animation;

    
    GameObject trigger;

    [SerializeField]
    Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        animation = canvas.GetComponent<Animation>();
        FadeIn = animation.GetClip("FadeIn");
        FadeOut = animation.GetClip("FadeOut");
        trigger = this.gameObject;


        if (trigger == null)
        {

            Debug.Log("trigger is not assigned");
            return;

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
     
        animation.Play("FadeOut");
        Debug.Log("enter");
    }
    private void OnTriggerExit(Collider other)
    {
        animation.Play("FadeIn");
        Debug.Log("exit");
        
    }

}