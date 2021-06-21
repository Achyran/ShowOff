using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelControl : MonoBehaviour
{
    [Tooltip("The speed at which the model will rotate into the direction the player is moving")]
    [SerializeField]
    private float rotateSpeed;
    public Transform player;


    //Modify this to look in to the velocety direction of the player
    public void ModelUpdate(Vector3 translation, Vector3 position)
    {
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, translation, rotateSpeed * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.position = position;
    }
}
