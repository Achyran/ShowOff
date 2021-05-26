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
	public LayerMask playerLayer;
	public float rotateSpeed = 1;
	private bool playerFrozen = false;
	private GameObject inspectingObject;
	private OutlineScript lastOutline;
	private Rigidbody rigidBody;

	class CameraState
	{
		public float yaw;
		public float pitch;
		public float roll;
		public float x;
		public float y;
		public float z;


		public void SetFromTransform(Transform t)
		{
			pitch = t.eulerAngles.x;
			yaw = t.eulerAngles.y;
			roll = t.eulerAngles.z;
			x = t.position.x;
			y = t.position.y;
			z = t.position.z;
		}

		public void Translate(Vector3 translation)
		{
			Vector3 rotatedTranslation = Quaternion.Euler(pitch, yaw, roll) * translation;

			x += rotatedTranslation.x;
			y += rotatedTranslation.y;
			z += rotatedTranslation.z;
		}

		public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
		{
			yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
			pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
			roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);

			x = Mathf.Lerp(x, target.x, positionLerpPct);
			y = Mathf.Lerp(y, target.y, positionLerpPct);
			z = Mathf.Lerp(z, target.z, positionLerpPct);
		}

		public void UpdateTransform(Transform t)
		{
			t.eulerAngles = new Vector3(pitch, yaw, roll);
			t.position = new Vector3(x, y, z);
		}
	}

	CameraState m_TargetCameraState = new CameraState();
	CameraState m_InterpolatingCameraState = new CameraState();

	[Header("Movement Settings")]
	[Tooltip("The speed at which you move in any direction")]
	public float speed = 3.5f;

	[Tooltip("Time it takes to interpolate camera position 99% of the way to the target."), Range(0.001f, 1f)]
	public float positionLerpTime = 0.2f;

	[Header("Rotation Settings")]
	[Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
	public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

	[Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
	public float rotationLerpTime = 0.01f;

	[Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
	public bool invertY = false;

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
	}
	void OnEnable()
	{
		m_TargetCameraState.SetFromTransform(transform);
		m_InterpolatingCameraState.SetFromTransform(transform);

		
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

	void Update()
	{
		Vector3 translation = Vector3.zero;


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

		if (!playerFrozen)
		{

			var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));

			var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

			m_TargetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor;
			m_TargetCameraState.pitch += mouseMovement.y * mouseSensitivityFactor;

			// Translation
			translation = GetInputTranslationDirection() * Time.deltaTime;

			translation *= speed;

			rigidBody.AddForce(translation);

			
			m_TargetCameraState.Translate(translation);

			Vector3 newDirection = Vector3.RotateTowards(transform.forward, translation, rotateSpeed * Time.deltaTime, 0.0f);

			transform.rotation = Quaternion.LookRotation(newDirection);
			/*
			// Framerate-independent interpolation
			// Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
			var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * Time.deltaTime);
			var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
			m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);

			//Only make movements if notepad isn't up

			m_InterpolatingCameraState.UpdateTransform(transform);
			*/

			/*
			RaycastHit hit;
			if (Physics.Raycast(mainCam.ScreenPointToRay(new Vector3(mainCam.pixelWidth / 2, mainCam.pixelHeight / 2)), out hit))
			{
				if (hit.transform.gameObject.tag == "Interactable" && Input.GetMouseButtonDown(0))
					//CameraSwitch(_hit.transform.gameObject.GetComponentInChildren<CinemachineFreeLook>());
					CameraSwitch(hit.transform.GetChild(0).gameObject);
			}
			*/
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit))
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



		}
		//Switching out of inspection mode
		else if (inspectingObject != null && Input.GetMouseButtonDown(0))
		{
			CameraSwitch(inspectingObject);
		}
		
	}

	public void FreezePlayer(bool val)
	{
		playerFrozen = val;
	}
	private void CameraSwitch(GameObject _object)
	{
		if (playerCam.gameObject.activeInHierarchy)
		{
			_object.gameObject.SetActive(true);
			playerCam.gameObject.SetActive(false);
			inspectingObject = _object;
			playerFrozen = true;
		}
		else
		{
			_object.gameObject.SetActive(false);
			playerCam.gameObject.SetActive(true);
			inspectingObject = null;
			playerFrozen = false;
		}
	}

	private void ToggleNotebook()
	{
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

