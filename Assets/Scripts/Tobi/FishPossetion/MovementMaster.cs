using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovementMaster : MonoBehaviour
{
    public static MovementMaster current;
    [SerializeField]
    private IPosessable currentContorl;

    // Start is called before the first frame update
    void Start()
    {
        if(current != null)
        {
            current = this;
        }
        else
        {
            Debug.LogWarning($"MovementMaster exist already: SeveDistruct", this);
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentContorl != null)
        {
            currentContorl.Move();
        }
    }

    public event Action event_Posess;

    public void Posess()
    {
        if(event_Posess != null)
        {

        }
    }
}
