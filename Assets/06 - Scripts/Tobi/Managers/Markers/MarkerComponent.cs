using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MarkerComponent : MonoBehaviour
{
    [SerializeField]
    private float delay;
    [SerializeField]
    private MarkerComponent nextMarker;
    [SerializeField]
    [Tooltip("This Activates this Marker after the delay, leave falls if it sould unlock after a different Marker")]
    public bool isActiveAtStart;


    private MeshRenderer ren;
    private bool isVisable = false;
    private bool isActive = false;
    private Transform cam;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        //gets Renderer and disables it;
        ren = GetComponent<MeshRenderer>();
        ren.enabled = false;
        cam = FindObjectOfType<Camera>().transform;
        if(cam == null)
        {
            Debug.LogWarning($"No player found. Rotation will no work");
        }
        if (isActiveAtStart)
        {
            StartMarker();
        }
        if(MarkerMaster.current != null)
        {
            MarkerMaster.current.onActivate += Actikvate;
        }
        
    }

    private void Actikvate(MarkerComponent obj)
    {
        if(obj == this)
        {
            StartMarker();
        }
    }

    private void Update()
    {
        if (isActive)
        {
            if (timer < 0)
            {
                isVisable = true;
                ren.enabled = true;
            }
            else timer -= Time.deltaTime;
            Rotate();
        }
        
    }

    public void StartMarker()
    {
        if (!isActive)
        {
            timer = delay;
            isActive = true;
        }
    }

    public void DestroyMarker()
    {
        if(nextMarker != null) MarkerMaster.current.Activate(nextMarker);
        Destroy(this.gameObject);
    }
    private void Rotate()
    {
        if (isVisable)
        {
            transform.rotation = Quaternion.LookRotation(cam.position - transform.position);
            transform.Rotate(new Vector3(90, 0, 0));
            
            
        }
    }

    void OnDestroy()
    {
        if (MarkerMaster.current != null)
        {
            MarkerMaster.current.onActivate -= Actikvate;
        }
    }

}
