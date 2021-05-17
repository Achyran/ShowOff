using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics Fliter")]
public class PhysicsFliter : CTXFilter
{
    [SerializeField]
    private LayerMask thisIsObsical;
    public override List<Transform> Filter(FlockAgent pAgent, List<Transform> pOriginal)
    {
        List<Transform> filterd = new List<Transform>();
        int ctxindex = pOriginal.Count;
        foreach (Transform item in pOriginal)
        {
            if (thisIsObsical == (thisIsObsical | (1 << item.gameObject.layer))) { filterd.Add(item); }
        }
        Debug.Log($"The list started wit {ctxindex} members. Now it has {filterd.Count}");
        return filterd;
    }
}
