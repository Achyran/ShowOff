using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumetricTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject volumetricToBeEnabled;

    [SerializeField]
    GameObject volumetricToBeDisabled;


    public bool Enable;
    public bool Disable;

    private void OnTriggerEnter(Collider other)
    {
        if(Disable) volumetricToBeDisabled.SetActive(false);
        if(enabled) volumetricToBeEnabled.SetActive(true);
    }

}
