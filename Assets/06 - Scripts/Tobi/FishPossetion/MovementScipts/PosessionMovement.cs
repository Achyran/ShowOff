using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PosessionMovement : MonoBehaviour
{
    //exposedVarables

    [SerializeField]
    [Tooltip("Overrides the stats at the start")]
    private PosessionMovementStats stats;

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
    [Range(0, 60)]
    public float posessionTime = 5;



    [Header("Funcutional Variabels")]
    [SerializeField]
    private bool freeze = true;
    private bool qtFreeze = false;
    [SerializeField]
    private bool loadStats = true;
    [SerializeField]
    [Tooltip("Saves the changes to the Stats when exiting the playmode")]
    private bool saveStats = false;


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

        if(stats != null && loadStats)
        {
            SetStats();
        }

        //Subscribe
        if (GameMaster.current != null)
        {
            GameMaster.current.onPosessionStart += StartPosession;
            GameMaster.current.onPosessionStop += StopPosession;
        }
        if (QuickTimeMaster.current != null)
        {
            QuickTimeMaster.current.onQuickTimeStart += StartQuicktime;
            QuickTimeMaster.current.onQuickTimeEnd += EndQuicktime;
        }
        
        rb.isKinematic = true;
    }
    //UnFreezes On QuickTime end
    private void EndQuicktime(QuickTimeComponent arg1, bool arg2)
    {
        qtFreeze = false;
        rb.isKinematic = false;
    }
    //Freezes on Qucicktime Start
    private void StartQuicktime(QuickTimeComponent obj)
    {
        qtFreeze = true;
        rb.isKinematic = true;
    }
    // Check if can Move and than move
    private void FixedUpdate()
    {
        if(canMove)
        {
            if (freeze || qtFreeze)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;

                //ToDo: Add Fish AI
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
    //Returns if Pitch is in the Defined clamp
    bool pitchInClap()
    {
        if (transform.rotation.eulerAngles.x < pitchClamp || transform.rotation.eulerAngles.x > 360 - pitchClamp)
            return true;
        return false;
    }
    //Sets Stateds form Scriptable object
    private void SetStats()
    {
        maxSpeed = stats.maxSpeed;
        speed = stats.speed;
        reverseDamp = stats.reverseDamp;
        pitchClamp = stats.pitchClamp;
        rotationSpeed = stats.rotationSpeed;
        rotationReturnDamp = stats.rotationReturnDamp;
        posessionTime = stats.posessionTime;

        if(rb != null)
        {
            rb.mass = stats.mass;
            rb.drag = stats.drag;
            rb.angularDrag = stats.AngularDrag;
        }
    }
    //Saves Stats to Scribtable object
    private void SaveStats()
    {
        if (stats != null)
        {
            stats.maxSpeed = maxSpeed;
            stats.speed = speed;
            stats.reverseDamp = reverseDamp;
            stats.pitchClamp = pitchClamp;
            stats.rotationSpeed = rotationSpeed;
            stats.rotationReturnDamp = rotationReturnDamp;
            stats.posessionTime = posessionTime;

            if (rb != null)
            {
                stats.mass = rb.mass;
                stats.drag = rb.drag;
                stats.AngularDrag = rb.angularDrag;
            }
        }
        else Debug.LogWarning("Stats could not be saved. Stats were Null", this);
    }
    //Saves stats on Application Quit
    private void OnApplicationQuit()
    {
        if (saveStats) SaveStats();
    }
    //Unfreezes if Posessed
    private void StartPosession(PosessionMovement posession)
    {
        if(this == posession)
        {
            freeze = false;
            rb.isKinematic = false;
        }
    }
    //Freezes On Poesssion End
    private void StopPosession()
    {
        freeze = true;
        rb.isKinematic = true;
    }
    //UnsubScribe
    private void OnDestroy()
    {
        GameMaster.current.onPosessionStart -= StartPosession;
        GameMaster.current.onPosessionStop -= StopPosession;
    }
}
