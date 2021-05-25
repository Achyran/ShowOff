using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/MoveToPoint")]
public class MoveToPointBehavoir : FlockBehavior
{
    [SerializeField]
    private Vector3 point;
    [SerializeField]
    [Range(0f, 10f)]
    private float strenght;
    [SerializeField]
    private float dist;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> ctx, Flock flock)
    {
        Vector3 offset = agent.transform.position - point;
        
        if(offset.sqrMagnitude < dist *dist)
        {
            return Vector3.zero; 
        }
        return offset.normalized * strenght;
        
    }
}
