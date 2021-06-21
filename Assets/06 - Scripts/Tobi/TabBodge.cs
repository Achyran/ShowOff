using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TabBodge : MonoBehaviour
{
    private bool isFirst = true;
    private TextMeshProUGUI text;
    private float timer;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (DiscoverableMaster.current != null) DiscoverableMaster.current.OnDiscover += DisplayText;
        text.enabled = false;
    }

    private void DisplayText(DiscoverableMaster.SpeciesInformation obj)
    {
        if (isFirst)
        {
            timer = 5;
            isFirst = false;
        }
    }
    private void Update()
    {
        if(timer > 0)
        {
            text.enabled = true;
            timer -= Time.deltaTime;
        }else
        {
            text.enabled = false;
        }
    }
}
