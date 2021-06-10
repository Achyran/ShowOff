using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class DialogueTrigger : MonoBehaviour
{

    [SerializeField]
    List<AudioClip> dialogueClips = new List<AudioClip>();
    [SerializeField]
    List<string> dialogueTexts = new List<string>();

    [SerializeField]
    TextMeshProUGUI dialogueTMP;

    AudioSource audioSource;
    AudioClip chosenDialogue;

    int random;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        random = Random.Range(0, dialogueClips.Count);
        chosenDialogue = dialogueClips[random];

        audioSource.clip = chosenDialogue;

       

       

        if(dialogueTMP == null)
        {
            Debug.Log("dialogueTMP in DialogueTrigger is null");
            return;
        }

        
        
        dialogueTMP.enabled = false;
        dialogueTMP.SetText(dialogueTexts[random]);

        Debug.Log(dialogueTMP.text);

    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            dialogueTMP.enabled = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("collision");
        audioSource.PlayOneShot(chosenDialogue);
        if (audioSource.isPlaying)
        {
            dialogueTMP.enabled = true;
            

        }
    }
}
