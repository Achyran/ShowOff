using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerPosessinTest : MonoBehaviour
{
    [SerializeField]
    private float posessableDist;
    [SerializeField]
    private bool debug;
    [SerializeField]
    private LayerMask whatIsPosessabel;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        if (GameMaster.current == null) Debug.LogError("A GameMaster is Needed for this Component", this);
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
    }

    private void CastRay()
    {
        if (Input.GetMouseButton(0) && GameMaster.current.canPosess)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (debug) Debug.DrawRay(ray.origin,ray.direction * posessableDist);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, posessableDist, whatIsPosessabel))
            {
                Debug.Log(hit.transform.name);
                PosessionMovement posession = hit.collider.GetComponent<PosessionMovement>();
                if(posession != null)
                {
                    GameMaster.current.PosessionStart(posession);
                    CamMaster.current.SetCam(hit.transform.gameObject);

                }

            }
        }
    }
}
