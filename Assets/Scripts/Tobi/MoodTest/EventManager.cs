using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager current;
    public int trashAmount { private set; get; }
    // Creat a sigleton referece to it selve, or selve destruct
    void Awake()
    {
        if(current == null)
        {
            current = this;
        }else
        {
            Debug.LogError("Multible Eventmanager found, Destroing this!");
            Destroy(this);
        }
    }

    //Fiers When ever trash is Created;
    public event Action event_TrashCreated;

    public void TrashCreated()
    {
        trashAmount++;
        if(event_TrashCreated != null)
        {
            event_TrashCreated();
        }
    }

    //Fiers Whene ever trash is collected
    public event Action event_TrashCollected;

    public void TrashCollected()
    {
        trashAmount--;
        if(event_TrashCollected != null)
        {
            event_TrashCollected();
        }
    }


}
