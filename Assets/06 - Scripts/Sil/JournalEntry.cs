using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class JournalEntry : MonoBehaviour
{
    
    public DiscoverableMaster.Species species;
    private DiscoverableMaster.SpeciesInformation info;

    public TMP_Text title;
    public TMP_Text entryText;
    private string descriptionText;


    public bool unlocked = false;

    void Start()
    {
        
         Initiate();
        
    }

    private void Initiate()
    {
        info = DiscoverableMaster.current.speciesToInfo[species];
        title.text = info.speciesname;
        descriptionText = info.description;

        if (unlocked)
            entryText.text = descriptionText;
        else
            entryText.text = "This entry has not yet been unlocked.";

    }

    public void UnlockEntry()
    {
        entryText.text = descriptionText;
        unlocked = true;
    }

}
