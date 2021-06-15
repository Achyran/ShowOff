using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private static _Scenes toBeLoaded = _Scenes.Empty;
    public static _Scenes currentScene = _Scenes.Scene1;
    public enum _Scenes
    {
        Scene1,Scene2,Scene3,LoadingScene,Empty
    }

    public static void LoadeScene(_Scenes scene)
    {
        if (toBeLoaded == _Scenes.Empty && scene != _Scenes.LoadingScene && scene != _Scenes.Empty)
        {

            SceneManager.LoadScene(_Scenes.LoadingScene.ToString());
            toBeLoaded = scene;
        }
    }

    internal static void Callback()
    {
        SceneManager.LoadScene(toBeLoaded.ToString());
        currentScene = toBeLoaded;
        toBeLoaded = _Scenes.Empty;
    }
}
