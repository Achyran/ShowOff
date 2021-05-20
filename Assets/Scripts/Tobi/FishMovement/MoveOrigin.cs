using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOrigin : MonoBehaviour
{
    [SerializeField]
    private StayOriginBehavior behavior;
    //Transforms the origin point of the behavior to its Transform
    void Update()
    {
        behavior.origin = transform.position;
    }
}
