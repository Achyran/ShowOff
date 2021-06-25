using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DiscoverableTrigger : MonoBehaviour
{
    [SerializeField]
    private DiscoverableMaster.Species species;
    private BoxCollider col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Calling Discover");
        if(other.gameObject == GameMaster.current.player.gameObject)
        {
            DiscoverableMaster.current.AddToDisvcovered(species);
        }
    }

    public void Discover()
    {
        DiscoverableMaster.current.AddToDisvcovered(species);
    }
}
