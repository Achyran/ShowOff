using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour, ICollectabel
{
    public Rigidbody rb;

    private void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
    }

    public void Collected() 
    {
        EventManager.current.TrashCollected();
        Destroy(this.gameObject);
    }
}
