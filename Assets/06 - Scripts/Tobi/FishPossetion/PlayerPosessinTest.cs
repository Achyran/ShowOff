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
    private OutlineScript lastOutline;
    [SerializeField]
    private KeyCode key;



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
        if (GameMaster.current.state == GameMaster.State._base)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (debug) Debug.DrawRay(ray.origin, ray.direction * posessableDist);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, posessableDist, whatIsPosessabel | whatIsInteractabel))
            {
                GameObject hitObj = hit.transform.gameObject;
                
                if(lastOutline == null || lastOutline.gameObject != hitObj)
                {
                     lastOutline = hitObj.GetComponent<OutlineScript>();
                     OutlineActivate(lastOutline);     
                }
                if (Input.GetKeyDown(key))
                {
                    
                    if (whatIsInteractabel == (whatIsInteractabel | (1 << hitObj.layer)))
                    {
                        GameMaster.current.InspectionStart(hitObj);
                    }
                    else if (whatIsPosessabel == (whatIsPosessabel | (1 << hitObj.gameObject.layer)))
                    {
                        PosessionMovement posmove = hitObj.GetComponent<PosessionMovement>();
                        if (posmove != null) GameMaster.current.PosessionStart(posmove);
                        else Debug.LogWarning($"Tried to posess {hitObj.transform.name}, PosessionMovement was null. Pleas add PosessionMovement or Remove from layer", hitObj);
                    }
                }
            }
            else OutlineDisable();
            
        }

        //Stop Posession / Interaction
        if(Input.GetKeyDown(key) && GameMaster.current.state != GameMaster.State._base)
        {
            switch (GameMaster.current.state)
            {
                case GameMaster.State._base:
                    break;
                case GameMaster.State._posessing:
                    GameMaster.current.PosessionStop();
                    break;
                case GameMaster.State._inspecting:
                    GameMaster.current.InspectionStop();
                    break;
                case GameMaster.State._transition:
                    break;
                default:
                    break;
            }
        }
    }



    private void OutlineActivate(OutlineScript outline) 
    {
        if (outline != null)
        {
            outline.ToggleOutline(true);
            lastOutline = outline;
        }
        else
        Debug.LogWarning("Object is on the interactible layer but doesn't have an outline script", this);

    }

    private void OutlineDisable()
    {
        if (lastOutline != null)
            lastOutline.ToggleOutline(false);

        lastOutline = null;
    }
}
