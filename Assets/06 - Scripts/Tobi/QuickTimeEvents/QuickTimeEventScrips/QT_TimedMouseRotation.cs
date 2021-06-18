using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuickTimeEvents/MouseRotation")]
public class QT_TimedMouseRotation : QuickTimeEvent
{
    //[SerializeField]
    //private RotationDirection direction = RotationDirection.clockwise;
    [SerializeField]
    private int rotationCount = 5;
    [SerializeField]
    private float time = 10;
    [SerializeField]
    private bool debug = false;

    private Vector2 startMouse;
    private float currentAngle;
    private float combindAngle;
    private int rotation;

    private float _time;

    private enum RotationDirection {clockwise, counterclockwise }


    public override void Start()
    {
        if (outcome == QuickTimeEvent.Outcome.ready) _Init();
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
        CalcRotatition();
       
    }

    private void CalcRotatition()
    {
        float calcAngle = Vector2.SignedAngle(startMouse, Input.mousePosition);
        if (calcAngle >= 90)
        {
            //startMouse = Input.mousePosition;
            combindAngle += calcAngle;
            while (combindAngle > 360)
            {
                combindAngle -= 360;
                rotation++;
                if (debug) Debug.Log($"Current Rotation Count : {rotation}");
            }
        }
        //Debug.Log($"CalcAngel: {calcAngle} Combind: {combindAngle}  Rotations: {rotation}");
        if(rotation > rotationCount)
        {
            outcome = Outcome.sucsess;
            if (debug) Debug.Log("Event was a sucsess");
        }
    }

    private void _Init()
    {
        rotation = 0;
        combindAngle = 0;
        startMouse = Vector2.zero;
        _time = time;
        outcome = QuickTimeEvent.Outcome.running;
        startMouse = Input.mousePosition;
        if (debug) Debug.Log($"Event triggerd Roatate mouse {rotationCount} times");
        
    }

    public override void SetTextAndTexture()
    {
        
    }
}
