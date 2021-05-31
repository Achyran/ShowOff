using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameMaster : MonoBehaviour
{

    public static GameMaster current;
    private Player player;
    private PosessionMovement[] posessions;
    private PosessionMovement currentposession;

    private float _time;
    private bool canPosess = true;

    private void Awake()
    {
        InitiateGameMaster();
        FindPlayer();
        GetPosessions();
    }

    private void Update()
    {
        if (!canPosess)
        {
            _time -= Time.deltaTime;
            if(_time <= 0)
            {
                PosessionStop();
            }
        }
    }

    private void GetPosessions()
    {
        posessions = GameObject.FindObjectsOfType<PosessionMovement>();
    }

    private void FindPlayer()
    {
        Player[] players = GameObject.FindObjectsOfType<Player>();
        if (players.Length <= 0)
        {
            Debug.LogWarning("No player could be found");
            return;
        } else if (players.Length > 1)
        {
            Debug.LogWarning($" Only one player per seene can exists Players found: {players.Length}");
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

    #region Events
    public event Action<PosessionMovement> onPosessionStart;
    public void PosessionStart(PosessionMovement posession)
    {
        if (canPosess)
        {
            _time = posession.posessionTime;
            canPosess = false;
            if (onPosessionStart != null)
            {
                onPosessionStart(posession);
            }
        }
    }
    public event Action onPosessionStop;
    public void PosessionStop()
    {
        canPosess = true;
        if(onPosessionStop != null)
        {
            onPosessionStop();
        }
    }

    #endregion

}
