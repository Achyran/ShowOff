using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvent : MonoBehaviour
{
    private BoxCollider triggerBox;
    public CamConnection cinematicCam;

    // Start is called before the first frame update
    void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            CamMaster.current.SetCam(cinematicCam);
    }

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag == "Player")
        {
            CamMaster.current.SetCam(cinematicCam);
        }
	}

	private void OnCollisionExit(Collision collision)
	{
        if (collision.gameObject.tag == "Player")
        {
            CamMaster.current.SetCam(CamMaster.current.playerConnection);
        }
    }
}
