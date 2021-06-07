using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/AvoidentPlayer")]
public class AvoidancePlayerBehavior : FlockBehavior
{
    [SerializeField]
    private bool debug;
    [SerializeField]
    private float playerDistance = 1.5f;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> ctx, Flock flock)
    {
        //chec if player is null
        if(flock.player == null)
        {
            if (debug) Debug.LogError($"The Player is not set", flock);
            return Vector3.zero;
        }

        //if The player is in range avoid him
        if(Vector3.SqrMagnitude(flock.player.position - agent.transform.position) < playerDistance * playerDistance)
        {
            if (debug) Debug.LogError($"Avoid Player");
            return agent.transform.position - flock.player.position;
        }

        return Vector3.zero;

        
    }
}