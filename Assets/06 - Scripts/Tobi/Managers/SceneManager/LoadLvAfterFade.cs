using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadLvAfterFade : MonoBehaviour
{
    [SerializeField]
    private Loader._Scenes scene;
    [SerializeField]
    private UnityEvent endTrigg;

    private Animation fadeout;
    private bool loade = false;
    void Start()
    {
        fadeout = GameObject.FindGameObjectWithTag("SceneFadeOut").GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeout.isPlaying)
        {
            loade = true;
        }else if (loade)
        {
            endTrigg.Invoke();
            Loader.LoadeScene(scene);
        }
    }

    public void StartAimation()
    {
        fadeout.Play();
    }
}
