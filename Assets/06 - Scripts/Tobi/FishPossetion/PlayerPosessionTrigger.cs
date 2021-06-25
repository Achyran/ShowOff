using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPosessionTrigger : MonoBehaviour
{
    [SerializeField]
    private float dist;
    [SerializeField]
    private KeyCode key;
    [SerializeField]
    private LayerMask whatIsPosessable;
    [SerializeField]
    private LayerMask whatIsInteracatbe;

    private void Update()
    {
        PosessOrInteackt();
        ReturnToPlayer();
    }
    private void PosessOrInteackt()
    {
        if (Input.GetKeyDown(key))
        {
            //Debug.Log($"Start check",this);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, dist, whatIsPosessable | whatIsInteracatbe);
            Debug.Log($"{hitColliders.Length} colliders found");
            if (hitColliders.Length > 0)
            {
                Collider col = ClosestCollider(hitColliders);
                if (whatIsInteracatbe == (whatIsInteracatbe | (1 << col.gameObject.layer)))
                {
                    GameMaster.current.InspectionStart(col.gameObject);
                }
                else if (whatIsPosessable == (whatIsPosessable | (1 << col.gameObject.layer)))
                {

                    PosessionMovement posmove = col.GetComponent<PosessionMovement>();
                    if (posmove != null) GameMaster.current.PosessionStart(posmove);
                    else Debug.LogWarning($"Tried to posess {col.transform.name}, PosessionMovement was null. Pleas add PosessionMovement or Remove from layer", col);
                }
            }
        }
    }
    private void ReturnToPlayer()
    {
        if (Input.GetKeyDown(key) && GameMaster.current.state == GameMaster.State._inspecting) GameMaster.current.InspectionStop();
        else if (Input.GetKeyDown(key) && GameMaster.current.state == GameMaster.State._posessing) GameMaster.current.PosessionStop();

    }

    private Collider ClosestCollider(Collider[] pContext)
    {
        //if no contexrt was 0 retun null if 1 retun the only collider
        if (pContext.Length == 0)
        {
            return null;
        }
        else if (pContext.Length == 1) return pContext[0];
        float bestDist = Mathf.Infinity;
        Collider bestCanidate = new Collider();
        foreach (Collider c in pContext)
        {
            float colldist = (transform.position - c.transform.position).magnitude;

            if (colldist < bestDist)
            {
                bestDist = colldist;
                bestCanidate = c;
            }
        }
        //Debug.Log($"Best Collider", bestCanidate);
        return bestCanidate;
    }
}
