#if ENABLE_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM_PACKAGE
#define USE_INPUT_SYSTEM
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.Controls;
#endif

using UnityEngine;
using Cinemachine;


public class Player : MonoBehaviour
{
	public GameObject notepad;
	public Camera mainCam;
	public CinemachineFreeLook playerCam;
	public float rotateSpeed = 1;
	public float inspectRange = 25;
	public ModelControl modelControl;
	private bool playerFrozen = false;
	private GameObject inspectingObject;
	private OutlineScript lastOutline;
	private Rigidbody rigidBody;


	[Header("Movement Settings")]
	[Tooltip("The speed at which you move in any direction")]
	public float speed = 3.5f;

	[Header("Controls")]
	public KeyCode ForwardsKey = KeyCode.W;
	public KeyCode BackwardsKey = KeyCode.S;
	public KeyCode RightKey = KeyCode.D;
	public KeyCode LeftKey = KeyCode.A;
	public KeyCode DownKey = KeyCode.LeftControl;
	public KeyCode UpKey = KeyCode.Space;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.freezeRotation = true;
		Cursor.lockState = CursorLockMode.Locked;

		if(GameMaster.current != null)
        {
			GameMaster.current.onPosessionStart += StartPosession;
			GameMaster.current.onPosessionStop += StopPosession;
        }
	}

    #region Tobi
	//This is neede for the gameMasterLogic
    private void StartPosession(PosessionMovement posession)
    {
		FreezePlayer(true);
    }
     
    private void StopPosession()
    {
		FreezePlayer(false);
    }
    #endregion

    void Update()
	{
		

		InputManager();
		

		if (!playerFrozen)
		{
			MovementCalc();
		}
		//Switching out of inspection mode
		else if (inspectingObject != null && Input.GetMouseButtonDown(0))
		{
			CameraSwitch(inspectingObject);
		}
		
	}

	private void InputManager()
	{
		// Exit Sample  
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		}

		if (Input.GetKeyDown(KeyCode.K))
		{
			SaveData();
		}

		if (Input.GetKeyDown(KeyCode.L))
		{
			LoadData();
		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			ToggleNotebook();
		}
	}

	private void MovementCalc()
	{
		Vector3 translation = Vector3.zero;

		// Translation
		translation = GetInputTranslationDirection() * Time.deltaTime;

		translation *= speed;

		rigidBody.AddForce(translation);

		modelControl.ModelUpdate(translation, transform.position);

		/*
		RaycastHit hit;
		if (Physics.Raycast(transform.position, mainCam.transform.forward, out hit))
		{

			if (hit.transform.gameObject.tag == "Interactable")
			{
				lastOutline = hit.transform.gameObject.GetComponent<OutlineScript>();
				lastOutline.outlineObject.gameObject.SetActive(true);

				if(Input.GetMouseButtonDown(0))
				//CameraSwitch(_hit.transform.gameObject.GetComponentInChildren<CinemachineFreeLook>());
					CameraSwitch(hit.transform.GetChild(0).gameObject);

			}
			else if (lastOutline != null)
				lastOutline.outlineObject.gameObject.SetActive(false);

		}
		else if (lastOutline != null)
			lastOutline.outlineObject.gameObject.SetActive(false);
		*/

		RaycastHit[] hits;
		//hits = Physics.RaycastAll(transform.position, mainCam.transform.forward, 9999999.0F);
		Ray ray = mainCam.ScreenPointToRay(new Vector3(mainCam.pixelWidth / 2, mainCam.pixelHeight / 2, 0));
		//Possable optimasation : check only for first hit, Implement Masks
		hits = Physics.RaycastAll(ray, inspectRange);
		for (int i = 0; i < hits.Length; i++)
		{
			RaycastHit hit = hits[i];


			if (hit.transform.gameObject.GetComponent<OutlineScript>() != null)
			{
				lastOutline = hit.transform.gameObject.GetComponent<OutlineScript>();
				if( lastOutline != null)
				lastOutline.outlineObject.gameObject.SetActive(true);


				if (Input.GetMouseButtonDown(0))
				{
					//CameraSwitch(_hit.transform.gameObject.GetComponentInChildren<CinemachineFreeLook>());
					//CameraSwitch(hit.transform.GetChild(0).gameObject);
				}


				break;
            }
			else if (lastOutline != null)

				lastOutline.outlineObject.gameObject.SetActive(false);
		}
		if (hits.Length == 0 && lastOutline != null)
		{
			lastOutline.outlineObject.gameObject.SetActive(false);
		}
	}

	Vector3 GetInputTranslationDirection()
	{
		Vector3 direction = new Vector3();
		if (Input.GetKey(ForwardsKey))
		{
			direction += mainCam.transform.forward;
		}
		if (Input.GetKey(BackwardsKey))
		{
			direction += mainCam.transform.forward * -1;
		}
		if (Input.GetKey(LeftKey))
		{
			direction += mainCam.transform.right * -1;
		}
		if (Input.GetKey(RightKey))
		{
			direction += mainCam.transform.right;
		}
		if (Input.GetKey(DownKey))
		{
			direction += mainCam.transform.up * -1;
		}
		if (Input.GetKey(UpKey))
		{
			direction += mainCam.transform.up;
		}
		return direction;
	}

	private void FreezePlayer(bool val)
	{
		playerFrozen = val;
	}

	//Legacy Aproche Use CamMaster.current.SetCam(CamConnection connection);
	private void CameraSwitch(GameObject _object)
	{
		OutlineScript outlineObj = _object.GetComponent<OutlineScript>();
		GameObject camObj = outlineObj.objectCam.gameObject;

		if (playerCam.gameObject.activeInHierarchy)
		{
			camObj.gameObject.SetActive(true);
			playerCam.gameObject.SetActive(false);
			inspectingObject = _object;
			playerFrozen = true;
		}
		else
		{
			camObj.gameObject.SetActive(false);
			playerCam.gameObject.SetActive(true);
			inspectingObject = null;
			playerFrozen = false;
		}

		if (CamMaster.current != null)
		{
			CamConnection connection = _object.GetComponent<CamConnection>();
			if (connection != null)
			{
				CamMaster.current.SetCam(connection);
			}
		}
	}

	public void ToggleNotebook()
	{
		if(notepad != null)
		if (notepad.activeInHierarchy)
		{
			notepad.SetActive(false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			playerFrozen = false;

		}
		else
		{
			notepad.SetActive(true);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			playerFrozen = true;
		}
	}
	private void SaveData()
	{
		SaveSystem.SaveData(this);
	}
	
	private void LoadData()
	{
		SaveFile data = SaveSystem.LoadData();

		Vector3 position;

		position.x = data.position[0];
		position.y = data.position[1];
		position.z = data.position[2];

		Debug.Log(position);

		transform.position = position;
	}
}

