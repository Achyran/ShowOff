using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    
    public static GameMaster current;
    private Player player;
    private PosessionMovement[] posessions;
    private PosessionMovement currentposession;


    private void Awake()
    {
        InitiateGameMaster();
        FindPlayer();
        GetPosessions();
    }
    private void GetPosessions()
    {
        posessions = GameObject.FindObjectsOfType<PosessionMovement>();
    }

    private void FindPlayer()
    {
        Player [] players = GameObject.FindObjectsOfType<Player>(); 
        if(players.Length <= 0)
        {
            Debug.LogError("No player could be found");
            return;
        }else if(players.Length > 1)
        {
            Debug.LogError($" Only one player per seene can exists Players found: {players.Length}");
            return;
        }
        player = players[0];
    }

    //Creates a sigleton reference to itself or serve distruces if one allready exists
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

    private string toString()
    {
        return "Not Implemetned jet";
    }
}
