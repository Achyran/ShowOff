using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    public List<GameObject> audios = new List<GameObject>();

    private void Start()
    {
        audios.AddRange(GameObject.FindGameObjectsWithTag("Audio"));
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject trigger in audios)
        {
            trigger.gameObject.SetActive(false);
        }
    }

}
