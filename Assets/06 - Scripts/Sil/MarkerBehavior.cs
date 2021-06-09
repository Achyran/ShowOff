using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerBehavior : MonoBehaviour
{
    bool moveUp = true;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (moveUp)
        {
            if (timer < 1)
            {
                timer += Time.deltaTime;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.004f, transform.position.z);
            }
            else
            {
                moveUp = false;
                timer = 0;
            }

        }
        else
        {
            if (timer < 1)
            {
                timer += Time.deltaTime;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.004f, transform.position.z);
            }
            else
            {
				moveUp = true;
                timer = 0;
            }
        }

    }
}
