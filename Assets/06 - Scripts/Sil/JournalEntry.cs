using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class JournalEntry : MonoBehaviour
{
    public JournalEntryData entryData;

    public TMP_Text title;
    public TMP_Text entryText;

    
    public GameObject unlockObject;

    public bool unlocked = false;

    // Update is called once per frame
    void Start()
    {
        title.text = entryData.title;
        if (unlocked)
            entryText.text = entryData.entry;
        else
            entryText.text = "This entry has not yet been unlocked.";

        /*
        if (entryData.unlockObject != null)
            unlockObject = entryData.unlockObject;
        */
    }

	private void Update()
	{
       
	}

    public void UnlockEntry()
    {
        entryText.text = entryData.entry;
        unlocked = true;
    }

}
