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

    [Header("Loade Save :")]
    [SerializeField]
    private string name;

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
        if(name != null && name != "" && name != "Progress")
        {
            string jsonString = "";
            foreach(string file in Directory.GetFiles( $"{ Application.dataPath}/10 - Other assets/SpeciesInfoJson/"))
            {
                if (file.EndsWith($"{name}.json"))
                {
                    jsonString = File.ReadAllText($"{ Application.dataPath}/10 - Other assets/SpeciesInfoJson/{name}.json");
                    break;
                }
            }
            DiscoverableMaster.SpeciesInformation info = JsonUtility.FromJson<DiscoverableMaster.SpeciesInformation>(jsonString);
            OutputName = name;
            species = info.species;
            speciesname = info.speciesname;
            description = info.description;
            image = info.image;
            UIText = info.UIText;
            UIimage = info.UIimage;

        }
    }
}
