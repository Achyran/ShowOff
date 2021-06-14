using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoverableAgent : MonoBehaviour
{
    [SerializeField]
    Species species;

    private void OnEnable()
    {
        GameMaster.current.onInspectionStart += SpeciesChecker;
    }

    private void SpeciesChecker(GameObject obj)
    {
        Debug.Log("SpeciesChecker");
    }



    private void OnDisable()
    {
        GameMaster.current.onInspectionStart -= SpeciesChecker;
    }


}
