using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ContainerGrab : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    float timeWindow;
    float time;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartChain()
    {
        time = 0;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time > 2f + timeWindow)
        {
            animator.SetBool("NotConnected",true);
        }
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E)&& 2f <time && time < 2f + timeWindow)
        {
            animator.SetBool("Connected",true);
        }
    }
}
