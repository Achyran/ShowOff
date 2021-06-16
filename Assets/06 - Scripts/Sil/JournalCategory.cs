using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class JournalCategory : MonoBehaviour
{
    [HideInInspector]
    public List<JournalEntry> entryList;

    [HideInInspector]
    public int entryIndex = 0;

    void Start()
    {
        //Does this go in the right order with hierarchy?
        foreach (Transform child in transform)
        {
            if (child.GetComponent<JournalEntry>() != null)
                entryList.Add(child.GetComponent<JournalEntry>());
            else
                Debug.LogError(child.name + " does not have a JournalEntry script attached");
        }

        entryList[entryIndex].gameObject.SetActive(true);
    }

	private void Update()
	{
       
	}

    public void iterateEntry(int amount)
    {
        entryList[entryIndex].gameObject.SetActive(false);

        entryIndex += amount;

        if (entryIndex + 1 > entryList.Count)
            entryIndex -= entryList.Count;
        else if (entryIndex < 0)
            entryIndex += entryList.Count;

        Debug.Log(entryIndex);

        entryList[entryIndex].gameObject.SetActive(true);


    }

}
