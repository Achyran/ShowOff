using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderComp : MonoBehaviour
{
    public Loader._Scenes scene;
    [SerializeField]
    private bool locked = false;

    public void LoadScene()
    {
        if(!locked)
        Loader.LoadeScene(scene);
    }
    public void Unlock()
    {
        locked = false;
    }
}
