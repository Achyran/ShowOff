using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{
    //This is Lagecy for debugging
    public static SceneSwitch current;


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("Start");
        if (current == null)
        {
            //Debug.Log("Multible SceneSwithcs Discoverd, destroying this", this);
            current = this;

        }
        else if (current != this) 
        {
            Destroy(this);
        }
    }

    

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) PreviusSceene();
        if (Input.GetKeyDown(KeyCode.RightArrow)) NextSceene();
    }

    public void NextSceene()
    {
        switch (Loader.currentScene)
        {
            case Loader._Scenes.TestLV1:

                Loader.LoadeScene(Loader._Scenes.TestLV3);
                break;
            case Loader._Scenes. TestLV3:

                Loader.LoadeScene(Loader._Scenes. TestLV2);
                break;
            case Loader._Scenes. TestLV2:

                Loader.LoadeScene(Loader._Scenes.TestLV1);
                break;
            case Loader._Scenes. LoadingScene:
                break;
            case Loader._Scenes.Empty:
                break;
            default:
                break;
        }
    }
    public void PreviusSceene()
    {


        switch (Loader.currentScene)
        {
            case Loader._Scenes.TestLV1:

                Loader.LoadeScene(Loader._Scenes.TestLV2);
                break;
            case Loader._Scenes. TestLV3:

                Loader.LoadeScene(Loader._Scenes.TestLV1);
                break;
            case Loader._Scenes. TestLV2:

                Loader.LoadeScene(Loader._Scenes. TestLV3);
                break;
            case Loader._Scenes. LoadingScene:
                break;
            case Loader._Scenes.Empty:
                break;
            default:
                break;
        }
    }
}
