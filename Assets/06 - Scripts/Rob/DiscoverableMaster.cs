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
       

        var agents = FindObjectsOfType<DiscoverableAgent>();

        for (int i = 0; i < agents.Length; i++)
        {
            DiscoverableAgents.Add(agents[i]);
        }

        AgentsRemaining = new List<DiscoverableAgent>(DiscoverableAgents);
    }

    private void Update()
    {
        
    }





}
