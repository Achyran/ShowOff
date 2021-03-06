#if ENABLE_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM_PACKAGE
#define USE_INPUT_SYSTEM
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.Controls;
#endif

using UnityEngine;
using Cinemachine;
using System;

public class Player : MonoBehaviour
{
	public GameObject notepad;
	public Camera mainCam;
	public float rotateSpeed = 1;
	public float inspectRange = 25;
	public ModelControl modelControl;
	private bool playerFrozen = false;
	private GameObject inspectingObject;
	private OutlineScript lastOutline;
	private Rigidbody rigidBody;
	private float speed;

	private bool isSwimming = false;
	private bool isFastSwimming = false;


	[SerializeField]
	Animator animator;

	[Header("Movement Settings")]
	[Tooltip("The speed at which you move in any direction")]
	public float movementSpeed = 400f;
	
	[Tooltip("The speed at which you move in any direction while sprinting")]
	public float sprintSpeed = 800f;

	[Header("Controls")]
	public KeyCode ForwardsKey = KeyCode.W;
	public KeyCode BackwardsKey = KeyCode.S;
	public KeyCode RightKey = KeyCode.D;
	public KeyCode LeftKey = KeyCode.A;
	public KeyCode DownKey = KeyCode.LeftControl;
	public KeyCode UpKey = KeyCode.Space;
	public KeyCode SprintKey = KeyCode.LeftShift;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.freezeRotation = true;
		Cursor.lockState = CursorLockMode.Locked;
		speed = movementSpeed;

		if(GameMaster.current != null)
        {
			GameMaster.current.onPosessionStart += StartPosession;
			GameMaster.current.onPosessionStop += StopPosession;
        }
		if(QuickTimeMaster.current != null)
        {
			QuickTimeMaster.current.onQuickTimeStart += StartQt;
			QuickTimeMaster.current.onQuickTimeEnd += EndQt;
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
	private void EndQt(QuickTimeComponent arg1, bool arg2)
	{
		FreezePlayer(false);
	}
	private void StartQt(QuickTimeComponent obj)
	{
		FreezePlayer(true);
	}

	#endregion

	void Update()
	{
		InputManager();
		if (!playerFrozen)
		{
			MovementCalc();
            if (rigidBody.velocity.magnitude < 1)
            {
                animator.SetBool("isFastSwimming", false);
                animator.SetBool("isSwimming", false);
            }
        }

		//Debug.Log("Velocity = " + rigidBody.velocity.magnitude);

		
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
		if (Input.GetKey(SprintKey))
        {
			speed = sprintSpeed;
            animator.SetBool("isFastSwimming", true);
            animator.SetBool("isSwimming", false);
        }
		else
        {

			speed = movementSpeed;
            animator.SetBool("isFastSwimming", false);
            animator.SetBool("isSwimming", true);


        }

		

		return direction;
	}

	private void FreezePlayer(bool val)
	{
		playerFrozen = val;
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

