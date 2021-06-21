using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(LoaderComp))]
public class LoadeEndAnimation : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector director;
    LoaderComp lodercom;

    private void Start()
    {
        lodercom = GetComponent<LoaderComp>();
    }

    //Triggers Loading of next scene on Animation end
    void Update()
    {
        if(director != null)
        {
            if (director.time >= director.duration - 0.1f || Input.GetKeyDown(KeyCode.Space))
                
                lodercom.LoadScene();

        }
    }
}
