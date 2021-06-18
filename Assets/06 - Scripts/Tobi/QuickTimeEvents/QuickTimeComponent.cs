using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuickTimeComponent : MonoBehaviour
{

    [Tooltip("Make shure that Every trigger has a unic sciptable object instance")]
    public QuickTimeEvent _event;
    [SerializeField]
    private bool destroyAfterOutcome;
    [SerializeField]
    private UnityEvent start;
    [SerializeField]
    private UnityEvent sucsess;
    [SerializeField]
    private UnityEvent failure;
    [SerializeField]
    private QuickTimeComponent precondition;
    [SerializeField]
    [Tooltip("Add the player or a posessable fish here if it is only suposed to be triggered by that, if left empty every one can trigger this event")]
    private GameObject triggerobject;

    private bool isDone = false;

    private void Start()
    {
        _event.outcome = QuickTimeEvent.Outcome.notReady;
        if (precondition == null) _event.outcome = QuickTimeEvent.Outcome.ready;
        _event.SetTextAndTexture();

    }


    // Update is called once per frame
    void Update()
    {
        _event.Run();
        Evaluate();
        //Debug.Log(Input.mousePosition);

        if (QuickTimeMaster.current != null)
        {
            QuickTimeMaster.current.onQuickTimeStart += QuickTimeStart;
            QuickTimeMaster.current.onQuickTimeEnd += QuickTimeEnd;
        }
        else Debug.Log("Quickt time events requier a Qucktime event Master, this was not found");
    }

    private void QuickTimeEnd(QuickTimeComponent component, bool outcome)
    {
        
        if(precondition != null && component == precondition && outcome && _event.outcome == QuickTimeEvent.Outcome.notReady) 
        { 
            _event.outcome = QuickTimeEvent.Outcome.ready;
        }
    }

    private void QuickTimeStart(QuickTimeComponent obj)
    {
        if(obj == this && _event.outcome == QuickTimeEvent.Outcome.ready)
        {
            _event.Start();
        }
    }

    private void Evaluate()
    {
        if (!isDone && _event.outcome != QuickTimeEvent.Outcome.running)
        {
            if (_event.outcome == QuickTimeEvent.Outcome.sucsess)
            {
                isDone = true;
                Sucsess();
                if (destroyAfterOutcome) Destroy(this.gameObject);
            }
            if (_event.outcome == QuickTimeEvent.Outcome.failure)
            {
                isDone = true;
                Failure();
                if (destroyAfterOutcome) Destroy(this.gameObject);
            }
        }
    }

    private void Sucsess()
    {
        sucsess.Invoke();
        QuickTimeMaster.current.QuickTimeEnd(this, true);
    }
    private void Failure()
    {
        failure.Invoke();
        QuickTimeMaster.current.QuickTimeEnd(this, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_event.outcome == QuickTimeEvent.Outcome.ready && (triggerobject == null || other.transform.gameObject == triggerobject))
        QuickTimeMaster.current.QuickTimeStart(this);
    }

    private void OnDestroy()
    {
        if (QuickTimeMaster.current != null)
        {
            QuickTimeMaster.current.onQuickTimeStart -= QuickTimeStart;
            QuickTimeMaster.current.onQuickTimeEnd -= QuickTimeEnd;
        }
    }
}
