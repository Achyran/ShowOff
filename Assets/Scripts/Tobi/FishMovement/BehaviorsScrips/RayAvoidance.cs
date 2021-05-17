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

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> ctx, Flock flock)
    {
        //Test if something Physical is infront
        Debug.DrawRay(agent.transform.position, agent.transform.forward * viewDist, Color.black);
        if(Physics.Raycast(agent.transform.position ,agent.transform.forward, viewDist, whatIsObstical))
        {
            FindFreePath(agent);
        }

        return Vector3.zero;
    }

    private Vector3 FindFreePath( FlockAgent agent)
    {
        bool foundAWay = false;
        //Shit I Where can i go ?
        for (int i = 1 ; i < TestRaycast.plottedPoints.Count; i++)
        {
            if (Physics.Raycast(agent.transform.position, TestRaycast.plottedPoints[i], viewDist, whatIsObstical))
            {
                Debug.DrawRay(agent.transform.position, TestRaycast.plottedPoints[i] * viewDist, Color.red);
            }
            else
            {
                if (!foundAWay)
                {
                    Debug.DrawRay(agent.transform.position, TestRaycast.plottedPoints[i] * viewDist, Color.green);
                    foundAWay = true;
                }
                else Debug.DrawRay(agent.transform.position, TestRaycast.plottedPoints[i] * viewDist, Color.blue);
                    //return TestRaycast.plottedPoints[i];
                    //Debug.Log($"Found with Index {i} the poit was {TestRaycast.plottedPoints[i]}");
                    // break;
            }
        }
        return Vector3.zero;
    }
}
