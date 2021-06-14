using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpeciesInfoTool : MonoBehaviour
{
   
    public string OutputName;

    public DiscoverableMaster.Species species;
    public string speciesname;
    [TextArea]
    public string description;
    public Texture2D image;
    public string UIText;
    public Texture2D UIimage;

    

    public void SaveInfo()
    {
        if(OutputName == null || OutputName == "" || OutputName.EndsWith(".json") || OutputName.EndsWith(".txt") || OutputName.EndsWith("Progress")) 
        {

            Debug.LogWarning("Coun not create file, pleas fill in an Outputname (without file extention)");
            return;    
        }
        DiscoverableMaster.SpeciesInformation info = new DiscoverableMaster.SpeciesInformation();
        info.species = species;
        info.speciesname = speciesname;
        info.description = description;
        info.image = image;
        info.UIText = UIText;
        info.UIimage = UIimage;
        File.WriteAllText($"{Application.dataPath}/10 - Other assets/SpeciesInfoJson/{OutputName}.json", JsonUtility.ToJson(info));
        Debug.Log($"Saved at {Application.dataPath}/10 - Other assets/SpeciesInfoJson/{OutputName}.json ");
    }

    public void LoadInfo()
    {
    
    }
}
