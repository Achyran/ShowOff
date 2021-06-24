using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public static _Scenes toBeLoaded = _Scenes.Empty;
    public static _Scenes currentScene = _Scenes.TestLV1;
    private static AsyncOperation loadingAsyncOp;

    private class LoadingMonobehavior : MonoBehaviour { };
    //Enum of all Scenes in built
    //To add a nother and the name here
    public enum _Scenes
    {
        TestLV1,TestLV2,TestLV3,Level1,Level2,Level3,Level4,EndScenePrototype,VisualReferenceLevel,Cinematic1,Cinematic2,Cinematic3,Cinematic5,Cinematic4,LoadingScene,Empty
    }
    //Call to Start loading a Scene
    public static void LoadeScene(_Scenes scene)
    {
        if (toBeLoaded == _Scenes.Empty && scene != _Scenes. LoadingScene && scene != _Scenes.Empty)
        {
            //Switch TO loading scene
            SceneManager.LoadScene(_Scenes.LoadingScene.ToString());
            toBeLoaded = scene;
        }
    }
    //Starts loading Sceen Async Returns null as if stil loading
    public static IEnumerator LoadingState(_Scenes scene)
    {
        yield return null;
        loadingAsyncOp =   SceneManager.LoadSceneAsync(scene.ToString());

        while (!loadingAsyncOp.isDone)
        {
            yield return null;
        }
    }
    //Returning Loading Progres betwen 0-1
    public static float LoadingProgres()
    {
        if (loadingAsyncOp != null)
        {
            return loadingAsyncOp.progress;
        }
        else return 1f;
    }
    // Creates an object to user the Coroutine
    internal static void Callback()
    {
        GameObject loadingObj = new GameObject("Loading Object");
        loadingObj.AddComponent<LoadingMonobehavior>().StartCoroutine(LoadingState(toBeLoaded));
        currentScene = toBeLoaded;
        toBeLoaded = _Scenes.Empty;
    }
}
