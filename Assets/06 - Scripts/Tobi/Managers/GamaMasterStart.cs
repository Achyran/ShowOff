using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaMasterStart : MonoBehaviour
{
    void Awake()
    {
        if (GameMaster.current != null) GameMaster.current.ScenneStart();
    }

}
