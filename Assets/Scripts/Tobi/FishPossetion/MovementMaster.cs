using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovementMaster : MonoBehaviour
{
    public static MovementMaster current;
    [SerializeField]
    private IPosessable currentContorl;
    [SerializeField]
    private float _possesingTime;


    // Start is called before the first frame update
    void Start()
    {
        if(current == null)
        {
            current = this;
        }
        else
        {
            Debug.LogWarning($"MovementMaster exist already: SeveDistruct", this);
            Destroy(this);
        }
    }

    // Update is called once per physicsUpdate
    void FixedUpdate()
    {

        if (currentContorl != null)
        {
            currentContorl.Move();
        }

        if (_possesingTime > 0)
        {
            _possesingTime -= Time.deltaTime;
        }else if(currentContorl != null)
        {
            RemovePosession();
        }


    }
    public void Posess(IPosessable posessable)
    {
        if (currentContorl == null && posessable != null)
        {
            currentContorl = posessable;
            _possesingTime = posessable.time;
            posessable.isPosessed = true;
        }
    }
    public void RemovePosession()
    {
        currentContorl.isPosessed = false;
        currentContorl = null;
    }
}
