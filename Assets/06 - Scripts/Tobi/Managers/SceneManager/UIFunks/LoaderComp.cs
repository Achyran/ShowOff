using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderComp : MonoBehaviour
{
    public Loader._Scenes scene;

    public void LoadScene()
    {
        Loader.LoadeScene(scene);
    }
}
