using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuickTimeEvents/TimedButton")]
public class QT_TimedButton : QuickTimeEvent
{
    [SerializeField]
    public bool canTrigger = true;
    [SerializeField]
    private KeyCode key = KeyCode.Space;
    [SerializeField]
    private float time = 10;
    [SerializeField]
    private bool debug = false;
    

    private float _time;

    //public QuickTimeEvent.Outcome outcome { get; set; } = QuickTimeEvent.Outcome.notReady;




    public override void Start()
    {
        if(outcome == QuickTimeEvent.Outcome.ready)_Init();
    }
    public override void Run()
    {
        if (outcome == QuickTimeEvent.Outcome.running) _Run();
    }

    private void _Run()
    {
        if(_time > 0)
        {
            _time -= Time.deltaTime;
        }
        else
        {
            if (debug) Debug.Log("The Quick time event was a failure");
            outcome = QuickTimeEvent.Outcome.failure;
        }

        if (Input.GetKeyDown(key))
        {
            if (debug) Debug.Log("The Quick time event was a Sucsess");

            outcome = QuickTimeEvent.Outcome.sucsess;
        }
    }

    private void _Init()
    {
        _time = time;
        outcome = QuickTimeEvent.Outcome.running;
        if(debug) Debug.Log($"QuicktimeEvent Got Started.\n Press :{ key} Time Left: { _time}");
    }
    
    
}
