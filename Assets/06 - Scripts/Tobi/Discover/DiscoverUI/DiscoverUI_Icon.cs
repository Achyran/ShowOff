using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class DiscoverUI_Icon : MonoBehaviour
{
    RawImage img;
    [SerializeField]
    private float displayTime = 5;
    private float _time;


    void Start()
    {
        img = GetComponent<RawImage>();
        img.enabled = false;
        if (DiscoverableMaster.current != null)
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
        _time = displayTime;
        if (obj.UIimage != null)
        {
            img.enabled = true;
            img.texture = obj.UIimage;
        }
    }

    void Update()
    {
        if (_time < 0)
        {
            img.enabled = false;
        }
        else
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
