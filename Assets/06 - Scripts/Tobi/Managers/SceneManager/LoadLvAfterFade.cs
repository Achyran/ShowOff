using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLvAfterFade : MonoBehaviour
{
    [SerializeField]
    private Loader._Scenes scene;
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
            Loader.LoadeScene(scene);
        }
    }

    public void StartAimation()
    {
        fadeout.Play();
    }
}
