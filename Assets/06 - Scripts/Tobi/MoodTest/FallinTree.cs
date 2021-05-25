using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallinTree : MonoBehaviour
{

    [SerializeField]
    private int threashhold;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null) Debug.LogError($"Animator is missing on {this}");
    }

    void Update()
    {
        animator.SetBool("IsStanding", isStanding());
    }
    private bool isStanding()
    {
        if (EventManager.current.trashAmount > threashhold)
        {
            return false;
        }
        else return true;
    }

    
}
