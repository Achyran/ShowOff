using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CTXFilter : ScriptableObject
{
    public abstract List<Transform> Filter(FlockAgent pAgent, List<Transform> pOriginal);
}
