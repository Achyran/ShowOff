using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventPosession : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                PosessionMovement posession = hit.collider.GetComponent<PosessionMovement>();
                if(posession != null)
                {
                    GameMaster.current.PosessionStart(posession);
                }
            }
        }
    }
}
