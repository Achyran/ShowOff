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

  
    TextMeshProUGUI dialogueTMP;

    AudioSource audioSource;
    AudioClip chosenDialogue;

    int random;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        dialogueTMP = this.GetComponentInChildren<TextMeshProUGUI>();
        if (dialogueTMP == null)
        {
            Debug.Log("dialogueTMP in DialogueTrigger is null");
            return;
        }
        dialogueTMP.enabled = false;
    }

    private void Update()
    {
        //Debug.Log("Audiosource  = " + audioSource.isPlaying + " on " + this.name);
        if (audioSource.isPlaying == false)
        {
            dialogueTMP.enabled = false;
            //Debug.Log("Playing?  = " + audioSource.isPlaying);
        }
        else if (audioSource.isPlaying == true)
        {
            dialogueTMP.enabled = true;
            //Debug.Log("Playing?  = " + audioSource.isPlaying);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        
        random = Random.Range(0, dialogueClips.Count);
        chosenDialogue = dialogueClips[random];

        audioSource.clip = chosenDialogue;
           
                
        dialogueTMP.SetText(dialogueTexts[random]);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(chosenDialogue);
        }
       

       // Debug.Log(dialogueTMP.text);

      

    }


   


}
