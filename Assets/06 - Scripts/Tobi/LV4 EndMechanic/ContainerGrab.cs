using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Animator))]
public class ContainerGrab : MonoBehaviour
{
    [SerializeField]
    Canvas explanationUI;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    float timeWindow;
    float time;
    bool isEvaluating;
    public enum State { ready,running,done}
    public State state { get; private set; }



    private void Start()
    {
        if(explanationUI != null)
        {
            explanationUI.enabled = false;
        }
    }
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
        if (Input.GetKeyDown(KeyCode.E) && isEvaluating)
        {
            isEvaluating = false;
            animator.SetTrigger("Connected");
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (isEvaluating)
        {
            explanationUI.enabled = false;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isEvaluating)
        {
            explanationUI.enabled = true;
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

