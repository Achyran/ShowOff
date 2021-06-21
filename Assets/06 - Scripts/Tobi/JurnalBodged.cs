using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JurnalBodged : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI entry;

    private int index;
    
    void Start()
    {
        if(DiscoverableMaster.current == null)
        {
            Debug.Log($"No discoverable Master found, destroyin this UI", this.gameObject);
            Destroy(gameObject);
        }
        
        


    }

    public void OnEnable()
    {
        if (DiscoverableMaster.current.progress == null || DiscoverableMaster.current.progress.Count == 0) DisplayEmpty();
        else DisplayIndex(index);
    }
    public void DisplayEmpty()
    {
        title.text = "No Enty Unlocked ";
        entry.text = "";
    }

    private void DisplayIndex(int index)
    {
        title.text = DiscoverableMaster.current.speciesToInfo[DiscoverableMaster.current.progress[index]].speciesname;
        entry.text = DiscoverableMaster.current.speciesToInfo[DiscoverableMaster.current.progress[index]].description;
    }

    public void LoadeNext()
    {
        if(index +1 < DiscoverableMaster.current.progress.Count)
        {
            index++;
            DisplayIndex(index);
        }
    }
    public void LoadPrevius()
    {
        if (index - 1 >= 0)
        {
            index--;
            DisplayIndex(index);
        }
    }
}
