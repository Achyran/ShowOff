using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSceneStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameMasterStart start = FindObjectOfType<GameMasterStart>();
        if (start != null) Debug.Log($"Found GameStart", start);
        else Debug.Log("Not Found");
    }


}
