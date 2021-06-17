using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalProgression : MonoBehaviour
{
    public static JournalProgression current;

    public JournalEntry[] entries;
    // Start is called before the first frame update
    void Start()
    {
        if (current == null)
        {
            current = this;
        }
        else
        {
            Debug.LogWarning("Multiple JournalProgression were detected, Destroying this", this);
            Destroy(this);
        }

        GetEntries();
    }

    private void GetEntries()
    {
        entries = FindObjectsOfType<JournalEntry>(true);
    }

    public void UnlockCheck()
    {
        foreach (DiscoverableMaster.Species species in DiscoverableMaster.current.progress)
        {
            foreach (JournalEntry entry in entries)
            {
                if (entry.species == species && !entry.unlocked)
                    entry.UnlockEntry();
            }
        }
    }



}
