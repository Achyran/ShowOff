using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallbalck : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            Debug.Log($"Calling back with Scenne to be loaded {Loader.toBeLoaded}");
            isFirstUpdate = false;
            if(Loader.toBeLoaded != Loader._Scenes.Empty)
            Loader.Callback();
        }
    }
}
