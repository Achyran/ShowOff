using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(GameMaster))]
public class DiscoverableMaster : MonoBehaviour
{
    public enum Species {whale, sailFish }
    //holds all informations on all Species that are implemented
    private Dictionary<Species, SpeciesInformation> speciesToInfo;
    private List<Species> progress;

    private string savePath = $"{Application.dataPath}/10 - Other assets/SpeciesInfoJson/Progress.json";
    private void Start()
    {
        InitInfos();
        LoadeProgress();

        GameMaster.current.onInspectionStart += AddToDescoverd;
    }

    private void AddToDescoverd(GameObject obj)
    {
        DiscoverableComponent disCom = obj.GetComponent<DiscoverableComponent>();
        if(disCom != null && !progress.Contains(disCom.species))
        {
            if (!speciesToInfo.ContainsKey(disCom.species))
            {
                Debug.Log($"Cound not Find {disCom.species} info, Are you missing a .json file?");
                return;
            }
            progress.Add(disCom.species);
            Debug.Log($"Added Species {disCom.species}");
        }
    }

    private void InitInfos()
    {
        speciesToInfo = new Dictionary<Species, SpeciesInformation>();
        string saveDirectory = $"{Application.dataPath}/10 - Other assets/SpeciesInfoJson/";
        foreach(string file in Directory.GetFiles(saveDirectory))
        {
            if (file.EndsWith(".json"))
            {
                string jsonString = File.ReadAllText(file);
                SpeciesInformation sInfo = JsonUtility.FromJson<SpeciesInformation>(jsonString);
                if (!speciesToInfo.ContainsKey(sInfo.species)) speciesToInfo.Add(sInfo.species, sInfo);
                else Debug.Log($"Multible infos for same species found, Ignorgin : {file} ");
            }
        }
    }

    private void LoadeProgress() 
    {
        progress = new List<Species>();
        if (File.Exists(savePath))
        {
            string jsonStrig = File.ReadAllText(savePath);
            progress = JsonUtility.FromJson<Progress>(jsonStrig).seenSpecies;
        }
        else Debug.Log("No discovery Saves Found");

    }
    private void SaveProgress()
    {
        Progress prog = new Progress();
        prog.seenSpecies = progress;
        string jsonString = JsonUtility.ToJson(prog);
        File.WriteAllText(savePath, jsonString);
    }

    #region SaveStrucs
    //This Class holds Species information For display and other purpesoses. The Json Files will be created through a Creat
    public struct SpeciesInformation
    {
        //Dont User getters and setters (it causes Problems with json)
        public Species species;
        public string speciesname;
        public string description;
        public Texture2D image;
        public string UIText;
        public Texture2D UIimage;
    }
    private class Progress
    {
        public List<Species> seenSpecies;
    }
    #endregion


    /*
        Hier starts Robs Lagecy code

    public List<DiscoverableComponent> DiscoverableAgents = new List<DiscoverableComponent>();
    public List<DiscoverableComponent> AgentsRemaining = new List<DiscoverableComponent>();
    

    // Start is called before the first frame update
    void Start()
    {
       

        var agents = FindObjectsOfType<DiscoverableComponent>();

        for (int i = 0; i < agents.Length; i++)
        {
            DiscoverableAgents.Add(agents[i]);
        }

        AgentsRemaining = new List<DiscoverableComponent>(DiscoverableAgents);
    }

    private void Update()
    {
        
    }
    */




}
