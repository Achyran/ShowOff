using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOrigin : MonoBehaviour
{
    [SerializeField]
    private StayOriginBehavior behavior;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        behavior.origin = transform.position;
    }
}
