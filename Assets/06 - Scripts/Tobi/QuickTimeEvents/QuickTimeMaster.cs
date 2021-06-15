using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(GameMaster))]
public class QuickTimeMaster : Master
{

    public static QuickTimeMaster current;
    public enum State { ready, inprogress };
    public State state { get; private set; }
    
    public override void Init()
    {
        if(current != null)
        {
            Debug.Log("Multibele QuickTimeMasters Detected, Destroing this", this);
            Destroy(this);
        }else
        {
            current = this;
        }

    }

    #region Events
    public event Action<QuickTimeComponent> onQuickTimeStart;

    public void QuickTimeStart(QuickTimeComponent component)
    {
        if (state == State.ready && onQuickTimeStart != null && component._event.outcome == QuickTimeEvent.Outcome.ready)
        {
            Debug.Log("Calling OnQuictimeStart");
            state = State.inprogress;
            onQuickTimeStart(component);
        }
    }
    public event Action<QuickTimeComponent, bool> onQuickTimeEnd;
    public void QuickTimeEnd(QuickTimeComponent component, bool outcome)
    {
        if (state == State.inprogress && onQuickTimeEnd != null)
        {
            state = State.ready;
            onQuickTimeEnd(component, outcome);
        }
    }

    public override void ScenneStart()
    {
    }

    #endregion

}
