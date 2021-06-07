using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuickTimeEvent : ScriptableObject
{
    public Outcome outcome { get; set; }
    public enum Outcome { notReady, ready, running, sucsess, failure }
    public abstract void Run();
    public abstract void Start();
}
