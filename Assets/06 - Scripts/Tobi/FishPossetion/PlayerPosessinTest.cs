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
    [SerializeField]
    private LayerMask whatIsInteractabel;
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
            RaycastHit posHit = new RaycastHit();
            RaycastHit interactHit = new RaycastHit();
            if(Physics.Raycast(ray, out hit, posessableDist, whatIsPosessabel))
            {
                posHit = hit;
            }
            if (Physics.Raycast(ray, out hit, posessableDist, whatIsInteractabel))
            {
                interactHit = hit;
            }
            besthit(posHit, interactHit);
        }
    }

    private void besthit(RaycastHit posHit, RaycastHit interHit)
    {
        if (posHit.distance == interHit.distance) return;
        if(posHit.distance > interHit.distance)
        {
            CamMaster.current.SetCam(interHit.transform.gameObject);
        }
        else
        {
            PosessionMovement posession = posHit.collider.GetComponent<PosessionMovement>();
            if (posession != null)
            {
                GameMaster.current.PosessionStart(posession);
                CamMaster.current.SetCam(posHit.transform.gameObject);
            }
        }
    }
}
