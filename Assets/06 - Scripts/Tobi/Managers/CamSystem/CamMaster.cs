using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(GameMaster))]
public class CamMaster : Master
{

    public int currentConnectionIndex { get; private set; } = -1;
    public static CamMaster current;
    public CamConnection[] connections { get; private set; }
    public CamConnection playerConnection;

    public override void Init()
    {
        InitiateGameMaster();
        GetConnections();
        GetPlayerConnections();
        if (playerConnection == null) Debug.LogWarning("The player connection was not set", this);
    }


    private void InitiateGameMaster()
    {
        if (current == null)
        {
            current = this;
        }
        else if( current != this)
        {
            Debug.LogWarning("Multible GameMasters detected, Destroying this", this);
            Destroy(this);
        }
    }
    private void GetConnections()
    {
        connections = GameObject.FindObjectsOfType<CamConnection>();
    }

    public event Action<CamConnection> onConnectionUpdate;
    public void SetCam(CamConnection connection)
    {
        
        if (onConnectionUpdate != null && currentConnectionIndex != Array.IndexOf(connections,connection))
        {
            onConnectionUpdate(connection);
            currentConnectionIndex = Array.IndexOf(connections, connection);
        }
        
    }


    public void SetCam(GameObject target)
    {
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
            Debug.Log("To many player Cam connections found, pleas make shur to tag only one");
        }
        else if (camConnections.Count <= 0)
        {
            Debug.Log("Cound not find player CamConnection, are you missing a Tag ?");
        }
        else
        {
            playerConnection = camConnections[0];
        }
    }

    public override void ScenneStart()
    {
        InitiateGameMaster();
        GetConnections();
        GetPlayerConnections();
    }
}
