using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class JournalDisplay : MonoBehaviour
{
    public JourneyEntry entryData;

    public TMP_Text title;
    public TMP_Text entryText;

    // Update is called once per frame
    void Start()
    {
        title.text = entryData.title;
        entryText.text = entryData.entry;
    }
}
