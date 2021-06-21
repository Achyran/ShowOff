using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (DiscoverableMaster.current != null) DiscoverableMaster.current.ResetSave();
    }

}
