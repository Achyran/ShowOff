using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFishInputTest : MonoBehaviour
{
    [SerializeField]
    private Transform target;

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
                target.position = hit.point + new Vector3(0,0.5f,0);


            }
        }
    }
}
