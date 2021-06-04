using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTTrigger : ProgressionTrigger
{
    [SerializeField]
    private ProgressionBlock block;

   public  void Trigger()
   {
        Triggerd(block);
   }
}
