using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(GameMaster))]
public class CamMaster : Master
{

    public int currentConnectionIndex { get; private set; } = -1;
    public static CamMaster current { get; private set; }
    public CamConnection[] connections { get; private set; }
    public CamConnection playerConnection { get; private set; }

    
    //Initialize CamMaster
    public override void Init()
    {
        InitiateCamMaster();
        //if (playerConnection == null) Debug.LogWarning("The player connection was not set", this);
    }
    //Call this to start in a sceene
    public override void ScenneStart()
    {
        GetConnections();
        GetPlayerConnections();
    }

    //Init Static Reverence
    private void InitiateCamMaster()
    {
        if (current == null)
        {
            current = this;
        }
        else if( current != this)
        {
            if(GameMaster.current.debug)  Debug.LogWarning("Multible GameMasters detected, Destroying this", this);
            Destroy(this);
        }
    }
    //Gets all Cam coneenctions in A scene
    private void GetConnections()
    {
        connections = GameObject.FindObjectsOfType<CamConnection>();
        //Set the startcam
        bool foundStartCam = false;
        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].isStartCam)
            {
                if (!foundStartCam)
                {
                    foundStartCam = true;
                    currentConnectionIndex = i;
                    connections[i].EnableVirtualCam();
                    //Debug.Log($"Start Cam", connections[i]);
                }
                else if(GameMaster.current.debug) Debug.Log($"Multible StartCamsFound.. Ignoring this", connections[i]);
            }
        }
        if (!foundStartCam && GameMaster.current.debug) Debug.LogWarning("No Startcam Found");

    }
    //Called Whene evere a camstate Changes
    public event Action<CamConnection> onConnectionUpdate;
    //Call this to Set a cam Activ
    public void SetCam(CamConnection connection)
    {
        if (onConnectionUpdate != null && currentConnectionIndex != Array.IndexOf(connections,connection))
        {
            onConnectionUpdate(connection);
            currentConnectionIndex = Array.IndexOf(connections, connection);
        }
        
    }

    //Check if Camm connection with Target exists and sets that connection
    public void SetCam(GameObject target)
    {
        if (GameMaster.current.debug) Debug.Log("Changing cam");
        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].target == target)
            {
                if (onConnectionUpdate != null)
                {
                    onConnectionUpdate(connections[i]);
                    currentConnectionIndex = i;
                }
                
                break;
            }

        }
        
    }


    private void GetPlayerConnections()
    {
        GameObject[] possibleConnections = GameObject.FindGameObjectsWithTag("PlayerConnection");
        List<CamConnection> camConnections = new List<CamConnection>();
        foreach (GameObject obj in possibleConnections)
        {
            CamConnection conec = obj.GetComponent<CamConnection>();
            if (conec != null)
            {
                camConnections.Add(conec);
            }
        }
        if (camConnections.Count > 1)
        {
            if (GameMaster.current.debug) Debug.Log("To many player Cam connections found, pleas make shur to tag only one");
        }
        else if (camConnections.Count <= 0)
        {
            if (GameMaster.current.debug) Debug.Log( $"Cound not find player CamConnection, are you missing a Tag ?");
        }
        else
        {
            playerConnection = camConnections[0];
        }
    }


}
