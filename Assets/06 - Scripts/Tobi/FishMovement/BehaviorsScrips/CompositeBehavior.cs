using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composit")]
public class CompositeBehavior : FlockBehavior
{
    public FlockBehavior[] behaviors;
    public float[] weights; 
    
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> ctx, Flock flock)
    {
        //handle data missmatch
        if (weights.Length != behaviors.Length) 
        { 
            Debug.LogError($"Data Missmatch in{name}", this);
            return Vector3.zero;    
        }
        Vector3 move = Vector3.zero;

        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector3 patialmove = behaviors[i].CalculateMove(agent, ctx, flock) * weights[i];
            if(patialmove != Vector3.zero)
            {
                if(patialmove.sqrMagnitude > weights[i] * weights[i])
                {
                    patialmove.Normalize();
                    patialmove *= weights[i];
                }
                move += patialmove;
            }
        }
        return move;
    }
}
