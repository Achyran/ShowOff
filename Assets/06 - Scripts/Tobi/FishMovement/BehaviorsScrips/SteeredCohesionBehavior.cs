using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/SteeredCohesion")]
public class SteeredCohesionBehavior : FilterdFlockBehavior
{
    [SerializeField]
    private float agentSomthTime = 0.5f;

    private Vector3 currentVelocety;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> ctx, Flock flock)
    {
        //Check for neghbors if = 0 no aductmetn
        if (ctx == null || ctx.Count == 0) return Vector3.zero;

        //add all vecs and avarage
        Vector3 calcmove = Vector3.zero;

        //Check if a filter exist, and if it is use it
        List<Transform> filteredctx = (filter == null) ? ctx : filter.Filter(agent, ctx);
        foreach (Transform t in filteredctx)
        {
            calcmove += t.position;
        }
        calcmove /= ctx.Count;

        //creat offset
        calcmove -= agent.transform.position;

        calcmove = Vector3.SmoothDamp(agent.transform.forward, calcmove, ref currentVelocety, agentSomthTime);

        return calcmove;
    }
}
