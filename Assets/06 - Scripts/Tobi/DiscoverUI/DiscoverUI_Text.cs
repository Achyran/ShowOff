using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DiscoverUI_Text : MonoBehaviour
{

    private TextMeshProUGUI display;
    [SerializeField]
    private float displayTime = 5;
    private float _time;

    void Start()
    {
        display = GetComponent<TextMeshProUGUI>();
        display.SetText("");
        if(DiscoverableMaster.current != null)
        {
            DiscoverableMaster.current.OnDiscover += ShowDiscover;
        }
        else
        {
            Debug.Log("DiscoverableMaser is needed, destroying this", this);
            Destroy(this);
        }
    }

    private void ShowDiscover(DiscoverableMaster.SpeciesInformation obj)
    {
        Debug.Log("Show On UI");
        _time = displayTime;
        display.SetText(obj.UIText);
    }

    private void Update()
    {
        if(_time < 0)
        {
            display.SetText("");
        }else
        {
            _time -= Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        if (DiscoverableMaster.current != null)
        {
            DiscoverableMaster.current.OnDiscover -= ShowDiscover;
        }
    }
}
