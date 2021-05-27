using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosessionMovement : MonoBehaviour
{
    //exposedVarables

    [Header("Movement Stads")]
    [SerializeField]
    private float maxSpeed = 5;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    [Range(0, 1)]
    private float reverseDamp = 0.5f;
    [SerializeField]
    [Range(0, 89)]
    private float pitchClamp = 45f;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    [Range(0,1)]
    private float rotationReturnDamp = 0.5f;



    [Header("Funcutional Variabels")]
    [SerializeField]
    private bool freeze;



    //HiddenVarabels

    private Rigidbody rb;
    private bool canMove = false;


    //PitchInputs
    private KeyCode[] pitchKeys = new KeyCode[2] {KeyCode.Space, KeyCode.LeftControl};

    void Start()
    {
        //Init rigidbody and null check
        rb = GetComponent<Rigidbody>();
        if (rb == null) Debug.LogError($"Rigidbody is missing", this);
        else canMove = true;
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            if (freeze)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                MoveHorizontal();
                RotateHeading();
                RotationPitch();
            }
        }
        
    }


    //Regulates the horizontal movement *Only call this in the fixed update
    private void MoveHorizontal()
    {   
        //For Forward input
        if (Input.GetAxis("Vertical") > 0 && rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * Input.GetAxis("Vertical") * speed);
        }
        //For BackwardInput
        if (Input.GetAxis("Vertical") < 0 && rb.velocity.magnitude < maxSpeed * reverseDamp)
        {
            rb.AddForce(transform.forward * Input.GetAxis("Vertical") * speed * reverseDamp);
        }
    }

    //Regulates the rotation in the heading
    private void RotateHeading()
    {
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.RotateAround(transform.position, Vector3.up, rotation);
    }
    //Regulates the PitchRotation
    private void RotationPitch()
    {
        //translate input to float
        float pitchRotation = 0f;
        if (Input.GetKey(pitchKeys[0])) pitchRotation -= 1.0f;
        if (Input.GetKey(pitchKeys[1])) pitchRotation += 1.0f;

        Debug.Log(transform.eulerAngles.x);
        if(pitchRotation != 0 && pitchInClap())
        {
            transform.Rotate(pitchRotation * rotationSpeed, 0, 0);
        }else if(pitchRotation == 0)
        {
            if(transform.eulerAngles.x > pitchClamp + 10  )
            {
                transform.Rotate(+rotationSpeed *reverseDamp, 0, 0);
            }else 
            if(transform.rotation.eulerAngles.x < 360 - (pitchClamp - 10) && transform.rotation.eulerAngles.x > rotationSpeed * reverseDamp)
            {
                transform.Rotate(-rotationSpeed * reverseDamp, 0, 0);
            }
        }


    }
    bool pitchInClap()
    {
        if (transform.rotation.eulerAngles.x < pitchClamp || transform.rotation.eulerAngles.x > 360 - pitchClamp)
            return true;
        return false;
    }
}
