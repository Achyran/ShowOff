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




	// Start is called before the first frame update
	void Start()
	{


		renderer = GetComponent<MeshRenderer>();
		
		

	}

	// Update is called once per frame
	void Update()
	{

	

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
}
	
