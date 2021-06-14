using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(GameMaster))]
public class CamMaster : MonoBehaviour
{

    public int currentConnectionIndex { get; private set; } = -1;
    public static CamMaster current;
    public CamConnection[] connections { get; private set; }
    public CamConnection playerConnection;

    void Awake()
    {
        InitiateGameMaster();
        GetConnections();
        if (playerConnection == null) Debug.LogWarning("The player connection was not set", this);
    }


    private void InitiateGameMaster()
    {
        if (current == null)
        {
            current = this;
        }
        else
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
   

    
   
}
