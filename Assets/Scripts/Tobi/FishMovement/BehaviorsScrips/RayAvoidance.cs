using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/RayAvoident")]
public class RayAvoidance : FilterdFlockBehavior
{
    [SerializeField]
    private LayerMask whatIsObstical;
    [SerializeField]
    private float viewDist;
    [SerializeField]
    bool DrawRayDebugg;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> ctx, Flock flock)
    {
        //Test if something Physical is infront
        if(DrawRayDebugg)Debug.DrawRay(agent.transform.position, agent.transform.forward * viewDist, Color.black);
        if(Physics.Raycast(agent.transform.position ,agent.transform.forward, viewDist, whatIsObstical))
        {
           return FindFreePath(agent);
        }

        return Vector3.zero;
    }

    private Vector3 FindFreePath( FlockAgent agent)
    {
        bool foundAWay = false;
        //Shit I Where can i go ?
        for (int i = 1 ; i < TestRaycast.plottedPoints.Count; i++)
        {
            Vector3 point = Quaternion.LookRotation(agent.transform.forward) * TestRaycast.plottedPoints[i] ; 
            if (Physics.Raycast(agent.transform.position, point , viewDist, whatIsObstical))
            {
               if(DrawRayDebugg) Debug.DrawRay(agent.transform.position, point * viewDist, Color.red);
            }
            else
            {
                if (!foundAWay)
                {
                    if(DrawRayDebugg) Debug.DrawRay(agent.transform.position, point * viewDist, Color.green);
                    foundAWay = true;
                    return point;
                }
                else if(DrawRayDebugg) Debug.DrawRay(agent.transform.position, point * viewDist, Color.blue);
                    //return TestRaycast.plottedPoints[i];
                    //Debug.Log($"Found with Index {i} the poit was {TestRaycast.plottedPoints[i]}");
                    // break;
            }
        }
        return Vector3.zero;
    }
}
