using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuickTimeComponent : MonoBehaviour
{
    public QuickTimeEvent _event;
    [SerializeField]
    private bool destroyAfterOutcome;
    [SerializeField]
    private UnityEvent start;
    [SerializeField]
    private UnityEvent sucsess;
    [SerializeField]
    private UnityEvent failure;
   

    private bool isDone = false;

    // Start is called before the first frame update
    void Start()
    {
        _event.outcome = QuickTimeEvent.Outcome.ready;
    }

    // Update is called once per frame
    void Update()
    {
        _event.Run();
        Evaluate();
        //Debug.Log(Input.mousePosition);
    }

    private void Evaluate()
    {
        if (!isDone && _event.outcome != QuickTimeEvent.Outcome.running)
        {
            if (_event.outcome == QuickTimeEvent.Outcome.sucsess)
            {
                isDone = true;
                sucsess.Invoke();
                if (destroyAfterOutcome) Destroy(this.gameObject);
            }
            if (_event.outcome == QuickTimeEvent.Outcome.failure)
            {
                isDone = true;
                failure.Invoke();
                if (destroyAfterOutcome) Destroy(this.gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        _event.Start();
    }

    public void Reset()
    {
        _event.outcome = QuickTimeEvent.Outcome.ready;
    }
}
