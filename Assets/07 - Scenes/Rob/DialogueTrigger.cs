using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{

    [SerializeField]
    AudioClip dialogueClip;
    [SerializeField]
    string dialogueText;

    TextMeshPro dialogueTMP;
    AudioSource audioSource;

    private void Start()
    {
        dialogueTMP = GameObject.FindGameObjectWithTag("dialogueUI").GetComponent<TextMeshPro>();
        if(dialogueTMP == null)
        {

            Debug.Log("dialogueTMP in DialogueTrigger is null");
            return;
        }
        
        dialogueTMP.enabled = false;
        dialogueTMP.text = dialogueText;

    }



    private void OnTriggerEnter(Collider other)
    {
        if (audioSource.isPlaying)
        {
            dialogueTMP.enabled = true;
            audioSource.PlayOneShot(dialogueClip);

        } else
        {

            dialogueTMP.enabled = false;
        }
    }
}
