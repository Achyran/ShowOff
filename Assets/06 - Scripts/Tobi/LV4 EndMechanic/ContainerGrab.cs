using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Animator))]
public class ContainerGrab : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    float timeWindow;
    float time;
    bool isEvaluating;
    public enum State { ready,running,done}
    public State state { get; private set; }

    public void StartEvaluation()
    {
        time = timeWindow;
        isEvaluating = true;
    }
    private void Update()
    {
       
        if (isEvaluating)
        {
            if(time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                isEvaluating = false;
                animator.SetTrigger("Disconnected");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isEvaluating = false;
            animator.SetTrigger("Connected");
        }
    }

    public void GotReset()
    {
        state = State.ready;
    }
    public void Ended()
    {
        state = State.done;
    }
    public void StartAnimation()
    {
        state = State.running;
        animator.SetTrigger("Start");
    }
}

