using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class FlockFIlter : CTXFilter
{
    public override List<Transform> Filter(FlockAgent pAgent, List<Transform> pOriginal)
    {
        List<Transform> filterd = new List<Transform>();

        foreach(Transform item in pOriginal)
        {
            FlockAgent flockAgent = item.GetComponent<FlockAgent>();
            if(flockAgent != null && flockAgent.agentFlock == pAgent.agentFlock)
            {
                filterd.Add(item);
            }
        }
        return filterd;
    }
}
  
