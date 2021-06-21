using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleTrigger : MonoBehaviour
{

    [SerializeField]
    List<GameObject> whales = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < whales.Count; i++)
        {
            whales[i].SetActive(false);
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < whales.Count; i++)
        {
            whales[i].SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for(int i = 0; i < whales.Count; i++)
        {
            Destroy(whales[i]);
        }
    }
}
