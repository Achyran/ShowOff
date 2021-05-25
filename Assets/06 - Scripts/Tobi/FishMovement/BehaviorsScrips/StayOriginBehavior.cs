using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/StayOrigin")]
public class StayOriginBehavior : FlockBehavior
{
    [SerializeField]
    public Vector3 origin;
    [SerializeField]
    private float raidus;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> ctx, Flock flock)
    {
        Vector3 centerOffset = origin - agent.transform.position;
        float t = centerOffset.magnitude / raidus;

        if(t < 0.9f) return Vector3.zero;

        return centerOffset * t * t;

    }
}
