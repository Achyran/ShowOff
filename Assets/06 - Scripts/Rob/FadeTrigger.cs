using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class FadeTrigger : MonoBehaviour
{


    RawImage rawImage;
    [SerializeField]
    Animation animation;

    private void Start()
    {
        rawImage = GameObject.FindGameObjectWithTag("SceneFadeOut").GetComponent<RawImage>();
        animation = GameObject.FindGameObjectWithTag("SceneFadeOut").GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        animation.Play();
        Debug.Log("animation played");
    }

}
