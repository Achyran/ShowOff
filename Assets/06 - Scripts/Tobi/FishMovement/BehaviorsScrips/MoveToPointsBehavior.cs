using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/MoveToPoitList")]
public class MoveToPointsBehavior : FlockBehavior
{
    [SerializeField]
    private Vector3[] point;
    [SerializeField]
    [Range(0f, 10f)]
    private float strenght;
    [SerializeField]
    private float radius;
    private int index;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> ctx, Flock flock)
    {
        Vector3 offset = point[index] - agent.transform.position;

        if (offset.sqrMagnitude < radius * radius)
        {
            index++;
            if(index >= point.Length)
            {
                index = 0;
            }

            return Vector3.zero;
        }
        return offset.normalized * strenght;

    }
}
