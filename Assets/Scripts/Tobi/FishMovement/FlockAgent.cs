using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    private Collider agentCollider;
    private Flock flock;
    public Flock agentFlock { get { return flock; } }
    public Collider AgentCollider { get { return agentCollider; } }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    public void Initialize(Flock pflock)
    {
        flock = pflock;
    }
    public void Move(Vector3 velocety)
    {
        if (velocety != Vector3.zero)
        transform.forward = velocety;
        transform.position += velocety * Time.deltaTime;
    }
}
