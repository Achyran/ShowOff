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
    public enum _Scenes
    {
        TestLV1,TestLV3,TestLV2,Level1, LoadingScene,Empty
    }

    public static void LoadeScene(_Scenes scene)
    {
        if (toBeLoaded == _Scenes.Empty && scene != _Scenes. LoadingScene && scene != _Scenes.Empty)
        {

            SceneManager.LoadScene(_Scenes.LoadingScene.ToString());
            toBeLoaded = scene;
        }
    }

    public static IEnumerator LoadingState(_Scenes scene)
    {
        yield return null;
        loadingAsyncOp =   SceneManager.LoadSceneAsync(scene.ToString());

        while (!loadingAsyncOp.isDone)
        {
            yield return null;
        }
    }

    public static float LoadingProgres()
    {
        if (loadingAsyncOp != null)
        {
            return loadingAsyncOp.progress;
        }
        else return 1f;
    }

    internal static void Callback()
    {
        GameObject loadingObj = new GameObject("Loading Object");
        loadingObj.AddComponent<LoadingMonobehavior>().StartCoroutine(LoadingState(toBeLoaded));
        currentScene = toBeLoaded;
        toBeLoaded = _Scenes.Empty;
    }
}
