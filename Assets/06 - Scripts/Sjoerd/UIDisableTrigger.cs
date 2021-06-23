using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisableTrigger : MonoBehaviour
{

    public GameObject uiText;

    public bool destroyafteruse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            uiText.SetActive(false);
        }
    }
}