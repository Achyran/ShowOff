using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Possession/Stats")]
public class PosessionMovementStats : ScriptableObject 
{
    [Header("Movement Stads")]
    [SerializeField]
    public float maxSpeed = 5;
    [SerializeField]
    public float speed = 1;
    [SerializeField]
    [Range(0, 1)]
    public float reverseDamp = 0.5f;
    [SerializeField]
    [Range(0, 89)]
    public float pitchClamp = 45f;
    [SerializeField]
    public float rotationSpeed;
    [SerializeField]
    [Range(0, 1)]
    public float rotationReturnDamp = 0.5f;

    [Header("Rigidbody Stats")]
    [SerializeField]
    public float mass = 1f;
    [SerializeField]
    public float drag = 5f;
    [SerializeField]
    public float AngularDrag = 7.72f;

}
