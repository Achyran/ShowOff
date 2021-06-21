using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuickTimeEvent : ScriptableObject
{
    public Texture UItexture { get; set; }
    public string UItext { get; set; }

    public Outcome outcome { get; set; }
    public enum Outcome { notReady, ready, running, sucsess, failure }
    public abstract void Run();
    public abstract void Start();
    public abstract void SetTextAndTexture();
}
