using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerPosessinTest : MonoBehaviour
{
    [SerializeField]
    private float posessableDist;
    [SerializeField]
    private bool debug;
    [SerializeField]
    private LayerMask whatIsPosessabel;
    [SerializeField]
    private LayerMask whatIsInteractabel;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        if (GameMaster.current == null) Debug.LogError("A GameMaster is Needed for this Component", this);
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
        ReturnToPlayer();
    }

    private void ReturnToPlayer()
    { 
       if (Input.GetMouseButtonDown(0) && GameMaster.current.state == GameMaster.State._inspecting) GameMaster.current.InspectionStop();

    }

    private void CastRay()
    {
        if (Input.GetMouseButton(0) && GameMaster.current.state == GameMaster.State._base)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (debug) Debug.DrawRay(ray.origin,ray.direction * posessableDist);
            RaycastHit hit;
            RaycastHit posHit = new RaycastHit();
            RaycastHit interactHit = new RaycastHit();
            if(Physics.Raycast(ray, out hit, posessableDist, whatIsPosessabel))
            {
                posHit = hit;
            }
            if (Physics.Raycast(ray, out hit, posessableDist, whatIsInteractabel))
            {
                interactHit = hit;
            }
            if (posHit.distance != 0 && interactHit.distance != 0)
                besthit(posHit, interactHit);
            else if (posHit.distance != 0) 
            {
                PosessionMovement posession = posHit.collider.GetComponent<PosessionMovement>();
                if (posession != null)
                {
                    GameMaster.current.PosessionStart(posession);
                }
            }else if(interactHit.distance != 0)
            {
                GameMaster.current.InspectionStart( interactHit.transform.gameObject);
            }

        }
    }

    private void besthit(RaycastHit posHit, RaycastHit interHit)
    {
        Debug.Log($"posHit was {posHit.distance} and InterHit was {interHit.distance} {posHit.distance > interHit.distance}");
        if (posHit.distance == interHit.distance) return;
        if(posHit.distance > interHit.distance)
        {
            GameMaster.current.InspectionStart(interHit.collider.gameObject);
        }
        else
        {
            PosessionMovement posession = posHit.collider.GetComponent<PosessionMovement>();
            if (posession != null)
            {
                GameMaster.current.PosessionStart(posession);
            }
        }
    }
}
