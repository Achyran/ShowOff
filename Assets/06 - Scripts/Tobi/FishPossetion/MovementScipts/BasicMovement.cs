using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour, IPosessable
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float posessionTime;
    public float time {get; set; }
    public bool isPosessed { get ; set ; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        time = posessionTime;
    }


    public void Move()
    {
        if (isPosessed)
        {
            rb.AddForce(transform.right * speed * Input.GetAxis("Horizontal"));
            rb.AddForce(transform.forward * speed * Input.GetAxis("Vertical"));
        }
        else rb.velocity = Vector3.zero;
    }
}
