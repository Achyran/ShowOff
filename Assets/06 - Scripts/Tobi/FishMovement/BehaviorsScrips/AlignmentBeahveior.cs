using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alingment")]
public class AlignmentBeahveior : FilterdFlockBehavior
{

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> ctx, Flock flock)
    {
        //Check for neghbors if = 0 no adjustions
        if (ctx == null || ctx.Count == 0) return agent.transform.forward;

        //add all vecs and avarage
        Vector3 calcmove = Vector3.zero;
        //Check if a filter exist, and if it is use it
        List<Transform> filteredctx = (filter == null) ? ctx : filter.Filter(agent,ctx);
        foreach (Transform t in filteredctx)
        {
            calcmove += t.transform.forward;
        }
        calcmove /= ctx.Count;

        return calcmove;
    }
}

