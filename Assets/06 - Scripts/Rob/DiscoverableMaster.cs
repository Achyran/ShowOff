using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoverableMaster : MonoBehaviour
{


    public List<DiscoverableComponent> DiscoverableAgents = new List<DiscoverableComponent>();
    public List<DiscoverableComponent> AgentsRemaining = new List<DiscoverableComponent>();
    

    // Start is called before the first frame update
    void Start()
    {
       

        var agents = FindObjectsOfType<DiscoverableComponent>();

        for (int i = 0; i < agents.Length; i++)
        {
            DiscoverableAgents.Add(agents[i]);
        }

        AgentsRemaining = new List<DiscoverableComponent>(DiscoverableAgents);
    }

    private void Update()
    {
        
    }





}
