using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelControl : MonoBehaviour
{
    public float rotateSpeed;
    public Transform player;

    
    // Update is called once per frame
    void LateUpdate()
    {

        
    }


    //Modify this to look in to the velocety direction of the player
    public void ModelUpdate(Vector3 translation, Vector3 position)
    {
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, translation, rotateSpeed * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.position = position;
    }
}
