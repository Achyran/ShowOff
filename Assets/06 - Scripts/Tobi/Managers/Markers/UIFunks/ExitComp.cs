using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitComp : MonoBehaviour
{
   public void Exit()
    {
        Application.Quit();
        Debug.Log("This is ignored in editor");
    }
}
