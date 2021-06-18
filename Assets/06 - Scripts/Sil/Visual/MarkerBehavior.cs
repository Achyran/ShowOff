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
	GameObject target;

	Material material;

	GameObject player;

	[SerializeField]
	float fadeDist = 30;

	bool MarkerNotActive;



	//--------------------------------

	
	void Start()
	{


		renderer = GetComponent<MeshRenderer>();
		renderer.enabled = false;

		//--------------- Rob ---------
		material = GetComponent<Renderer>().sharedMaterial;
		player = GameObject.FindGameObjectWithTag("Player");
		
		
		GameMaster.current.onInspectionStart += DisableMarker;


		//--------------------

	}

    private void OnEnable()
    {
		FadeIn();
		MarkerNotActive = false;

    }

    //------------- Rob --------------------
    void DisableMarker(GameObject obj)
	{
		if (target != null && obj == target)
		{
			renderer.enabled = false;
			Debug.Log("marker is disabled");
			MarkerNotActive = true;
		}

	}


	void DistanceFade()
    {

		float distance = Vector3.Distance(transform.position, player.transform.position);

		if(distance < fadeDist)
        {
			float distRatio = distance / fadeDist;
			Color alpha = new Color(material.color.r, material.color.g, material.color.b, distRatio); 

			material.SetColor("_BaseColor", alpha);
			
        } 

    }
	void FadeIn()
    {
		float alphaFloat = 0;
		alphaFloat++;
		Color FadeIn = new Color(material.color.r, material.color.g, material.color.b, alphaFloat);
		while(alphaFloat != 1)
        {
			material.SetColor("_BaseColor", FadeIn);
		}
		
	}


	// ------------------------------
	




	void Update()
	{

		if (MarkerNotActive == true)
		{
			renderer.enabled = false;
		}

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
    private void OnDisable()
    {
		MarkerNotActive = true;
		GameMaster.current.onInspectionStart -= DisableMarker;
	}
	//-------------------------------
}
	
