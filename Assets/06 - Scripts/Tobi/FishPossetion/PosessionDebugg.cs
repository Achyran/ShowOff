using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosessionDebugg : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(ray.origin, hit.point);
                IPosessable posessable = hit.collider.GetComponent<IPosessable>();
                if (posessable != null)
                {
                    MovementMaster.current.Posess(posessable);
                }


            }
        }
    }
}
