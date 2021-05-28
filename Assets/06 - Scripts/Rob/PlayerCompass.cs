using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCompass : MonoBehaviour
{
    public float playerNorth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerNorth = gameObject.transform.rotation.y;
        Debug.Log("PlayerNorth = " + playerNorth);
    }
}
