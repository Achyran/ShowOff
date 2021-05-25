using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)|| Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                Debug.DrawLine(ray.origin, hit.point);
                ICollectabel colletabel = hit.collider.GetComponent<ICollectabel>();
                if (colletabel != null)
                {
                    colletabel.Collected();
                }
                
                
            }
        }       
    }
}
