using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPosessable
{
    public float time { get; set; }
    public bool isPosessed { get; set; }
    public void Move();
}
