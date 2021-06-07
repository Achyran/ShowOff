using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/RayAvoident")]
public class RayAvoidanceBehavior : FilterdFlockBehavior
{
    [SerializeField]
    private LayerMask whatIsObsticle;
    [SerializeField]
    private float viewDistance;
    [SerializeField]
    bool drawRayDebug;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> ctx, Flock flock)
    {
        //Test if something Physical is infront
        if(drawRayDebug)Debug.DrawRay(agent.transform.position, agent.transform.forward * viewDistance, Color.black);
        if(Physics.Raycast(agent.transform.position ,agent.transform.forward, viewDistance, whatIsObsticle))
        {
           return FindFreePath(agent,flock);
        }

        return Vector3.zero;
    }

    private Vector3 FindFreePath( FlockAgent agent, Flock flock)
    {
        bool foundAWay = false;
        //Shit I Where can i go ?
        for (int i = 1 ; i < flock.plottedPoints.Count; i++)
        {
            Vector3 point = Quaternion.LookRotation(agent.transform.forward) * flock.plottedPoints[i] ; 
            if (Physics.Raycast(agent.transform.position, point , viewDistance, whatIsObsticle))
            {
               if(drawRayDebug) Debug.DrawRay(agent.transform.position, point * viewDistance, Color.red);
            }
            else
            {
                if (!foundAWay)
                {
                    if(drawRayDebug) Debug.DrawRay(agent.transform.position, point * viewDistance, Color.green);
                    foundAWay = true;
                    return point;
                }
                else if(drawRayDebug) Debug.DrawRay(agent.transform.position, point * viewDistance, Color.blue);
                    //return TestRaycast.plottedPoints[i];
                    //Debug.Log($"Found with Index {i} the poit was {TestRaycast.plottedPoints[i]}");
                    // break;
            }
        }
        return Vector3.zero;
    }
}
