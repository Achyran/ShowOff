using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoident")]
public class AvoidentBehavior : FilterdFlockBehavior
{

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> ctx, Flock flock)
    {
        //Check for neghbors if = 0 no adjustions
        if (ctx == null || ctx.Count == 0) return Vector3.zero;

        //add all vecs and avarage
        Vector3 calcmove = Vector3.zero;
        int nAvoid = 0;
        //Check if a filter exist, and if it does use it
        if(filter != null)
        {
            int ctxindex = ctx.Count;
            ctx = filter.Filter(agent, ctx);
        }
        foreach (Transform t in ctx)
        {
            if (Vector3.SqrMagnitude(t.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                calcmove += agent.transform.position - t.position;
            }
        }
        if(nAvoid > 0) calcmove /= nAvoid;

        return calcmove;
    }
}

