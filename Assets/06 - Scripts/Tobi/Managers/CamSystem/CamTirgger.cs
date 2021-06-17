using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CamTirgger : MonoBehaviour
{
    [SerializeField]
    private CamConnection connection;
    private BoxCollider box;
    private Collider player;
    private bool isIntrigger;
    private bool enterdTrigger;

    private void Start()
    {
        box = GetComponent<BoxCollider>();
        box.isTrigger = true;
        player = FindObjectOfType<Player>().GetComponent<Collider>();
    }
    /*
    private void Update()
    {

        //Debug.Log($"CammConection was null : {connection == null}");
        //Debug.Log($"CamMaster was null : {CamMaster.current == null}");
        

        if(isIntrigger && !connection.virtualCam.enabled && CamMaster.current != null)
        {
            CamMaster.current.SetCam(connection);
        }else if (!isIntrigger && enterdTrigger && connection.virtualCam.enabled && CamMaster.current != null)
        {
            Debug.Log($"Set back To player");
            CamMaster.current.SetCam(CamMaster.current.playerConnection);
            enterdTrigger = false;
            
        }

    }
    */

    private void FixedUpdate()
    {
        if(isIntrigger && !connection.virtualCam.enabled)
        {
            CamMaster.current.SetCam(connection);
        }else if (!isIntrigger && connection.virtualCam.enabled)
        {
            CamMaster.current.SetCam(CamMaster.current.playerConnection);
        }




        isIntrigger = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other == player)
        {
            isIntrigger = true;
        }
    }

}
