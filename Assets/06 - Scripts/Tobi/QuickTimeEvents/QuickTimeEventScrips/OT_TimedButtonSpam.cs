using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuickTimeEvents/TimedButtonSpam")]
public class OT_TimedButtonSpam : QuickTimeEvent
{
    [Header("UI Elements")]
    [SerializeField]
    public Texture _UItexture;
    [SerializeField]
    public string _UItext;

    [Header("Event Settings")]
    [SerializeField]
    private KeyCode key = KeyCode.Space;
    [SerializeField]
    private int buttonPresses = 10;
    [SerializeField]
    private float time = 10;
    [SerializeField]
    private bool debug = false;


    private float _time;
    private int buttonCounter;

    //public QuickTimeEvent.Outcome outcome { get; set; } = QuickTimeEvent.Outcome.notReady;




    public override void Start()
    {
        if (outcome == QuickTimeEvent.Outcome.ready)
        {
            _Init();
        }

        
        
    }
    public override void Run()
    {
        if (outcome == QuickTimeEvent.Outcome.running) _Run();
    }

    private void _Run()
    {
        if (_time > 0)
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
            buttonCounter++;

            if (buttonCounter >= buttonPresses)
            {
                if (debug) Debug.Log("The Quick time event was a Sucsess",this);

                outcome = QuickTimeEvent.Outcome.sucsess;
            }
        }
    }

    private void _Init()
    {
        _time = time;
        buttonCounter = 0;
        outcome = QuickTimeEvent.Outcome.running;
        if (debug) Debug.Log($"QuicktimeEvent Got Started.\n Press :{ key} Time Left: { _time}");
    }

    public override void SetTextAndTexture()
    {
        UItext = _UItext;
        UItexture = _UItexture;
    }
}
