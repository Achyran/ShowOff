using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaMasterStart : MonoBehaviour
{
    bool triggerd;
    void Awake()
    {
        if (GameMaster.current != null && !triggerd)
        {
            triggerd = true;
            GameMaster.current.ScenneStart();
        }
    }
    private void Start()
    {
        if (GameMaster.current != null && !triggerd)
        {
            triggerd = true;
            GameMaster.current.ScenneStart();
        }

        if (!triggerd) Debug.LogError("Could not inform GameMaster");
    }

}
