using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoverableMaster : MonoBehaviour
{


    public List<DiscoverableAgent> DiscoverableAgents = new List<DiscoverableAgent>();
    public List<DiscoverableAgent> AgentsRemaining = new List<DiscoverableAgent>();
    

    // Start is called before the first frame update
    void Start()
    {
        AgentsRemaining = new List<DiscoverableAgent>(DiscoverableAgents);

        var agents = FindObjectsOfType<DiscoverableAgent>();

        for (int i = 0; i < agents.Length; i++)
        {
            DiscoverableAgents.Add(agents[i]);
        }
    }

    private void OnEnable()
    {
       GameMaster.current.onInspectionStart += SpeciesChecker;
    }

    private void SpeciesChecker(GameObject obj)
    {



        Debug.Log("SpeciesChecker");
    }



    private void OnDisable()
    {
        GameMaster.current.onInspectionStart -= SpeciesChecker;
    }

}
