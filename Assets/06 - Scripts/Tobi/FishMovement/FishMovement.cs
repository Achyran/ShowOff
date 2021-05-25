using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour 
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float angle;
    [SerializeField]
    private float finishDist;

    private  Vector3 direction;

    // Start is called before the first frame update
    void Update()
    {
        Rotate();
        Debug.DrawRay(transform.position, transform.forward, new Color(1, 1, 1));
        Move();
    }

    private void Rotate()
    {
        Vector3 relavitpos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relavitpos);

        Quaternion current = transform.localRotation;

        transform.localRotation = Quaternion.Lerp(current, rotation, Time.deltaTime);
    }
    private void Move()
    {
        Vector3 relavitpos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relavitpos);
        if (angle > Quaternion.Angle(rotation, transform.rotation) && relavitpos.magnitude >= finishDist)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
    }
}
