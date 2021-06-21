using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public RawImage compassImage;
    public Transform mainCam;

    public Image objectiveMarker;
    private Vector2 markerPos;

    float compassUnit;


	private void Start()
	{
        compassUnit = compassImage.rectTransform.rect.width / 360f;

        if (mainCam == null)
            Debug.LogError("Please set the main camera in the compass inspector");
	}

	// Update is called once per frame
	void Update()
    {
        compassImage.uvRect = new Rect(mainCam.localEulerAngles.y / 360f, 0f, 1f, 1f);

        objectiveMarker.rectTransform.anchoredPosition = GetPosOnCompass();
    }

    Vector2 GetPosOnCompass() 
    {
        Vector2 playerPos = new Vector2(mainCam.transform.position.x, mainCam.transform.position.z);
        Vector2 playerFwd = new Vector2(mainCam.transform.forward.x, mainCam.transform.forward.z);

        float angle = Vector2.SignedAngle(markerPos - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }

    public void SetMarkerPos(MarkerComponent marker)
    {
        markerPos = new Vector2(marker.gameObject.transform.position.x, marker.gameObject.transform.position.z);
        Debug.Log("asdnaosjcnoa");

    }
}
