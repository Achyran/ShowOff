using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerBehavior : MonoBehaviour
{
	private bool moveUp = true;
	private float moveTimer = 0;
	public float activationTimer = 180;
	private bool active = false;

	MeshRenderer renderer;

	//------------ Rob --------------
	[SerializeField]
	GameObject InspectableTarget;

	[SerializeField]
	GameObject NextMarker;

	Material material;

	GameObject player;

	[SerializeField]
	float fadeDist = 30;

	public bool UsesInspectableObjectAsParameter = false;
	//--------------------------------

	
	void Start()
	{


		renderer = GetComponent<MeshRenderer>();
		renderer.enabled = false;

		//--------------- Rob ---------
		material = GetComponent<Renderer>().sharedMaterial;
		if(material == null)
        {
			Debug.Log("material is null");
			return;

        }

		player = GameObject.FindGameObjectWithTag("Player");
		GameMaster.current.onInspectionStart += DisableMarker;

		NextMarker.SetActive(false);
		//--------------------

	}

	// ------------ Rob --------------
	void DisableMarker(GameObject obj)
	{
		if (UsesInspectableObjectAsParameter)
		{
			if (InspectableTarget != null && obj == InspectableTarget)
			{
				renderer.enabled = false;
				Debug.Log("marker is disabled");

			}
		} else if (UsesInspectableObjectAsParameter == false)
        {
			Debug.Log("Not using a Inspectable Object as parameter");
        }
		

	}

	private void OnEnable()
    {

		active = false;

    }


    private void OnTriggerEnter(Collider other)
    {
		
		NextMarker.SetActive(true);
		Destroy(this.gameObject);
	}


    void DistanceFade()
    {

		float distance = Vector3.Distance(transform.position, player.transform.position);
		Debug.Log(distance);

		if(distance < fadeDist)
        {
			float distRatio = distance / fadeDist;
			Color alpha = new Color(material.color.r, material.color.g, material.color.b, distRatio);
			Debug.Log(distRatio);

			material.SetColor("_BaseColor", alpha);
			
        }
        else
        {
			material.SetColor("_BaseColor", new Color(material.color.r, material.color.g, material.color.b, 1));
        }

    }
	//---------------------------------






	void Update()
	{

		DistanceFade();

        if (!active)
		{
			if (activationTimer > 0)
				activationTimer -= Time.deltaTime;
			else
			{
				active = true;
				renderer.enabled = true;
			}
		}

		if (moveUp)
		{
			if (moveTimer < 1)
			{
				moveTimer += Time.deltaTime;
				transform.position = new Vector3(transform.position.x, transform.position.y + 0.004f, transform.position.z);
			}
			else
			{
				moveUp = false;
				moveTimer = 0;
			}

		}
		else
		{
			if (moveTimer < 1)
			{
				moveTimer += Time.deltaTime;
				transform.position = new Vector3(transform.position.x, transform.position.y - 0.004f, transform.position.z);
			}
			else
			{
				moveUp = true;
				moveTimer = 0;
			}
		}

		



	}
	// -------------- Rob --------------
    private void OnDestroy()
    {


		
		
			GameMaster.current.onInspectionStart -= DisableMarker;
		
	}
	//-------------------------------
}
	
