using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameMaster : Master
{


    public static GameMaster current;
    public Player player { get; private set; }
    private PosessionMovement[] posessions;
    private PosessionMovement currentposession;
    private float _time;
    public enum State { _base, _posessing, _inspecting, _transition}

    public State state { get; private set; } = State._base;
    private State nextState = State._transition;
    private float transitionTime = 0.5f;
    private float _transitionTime;

    [Tooltip("Turns all debugmessagees on / off on all masters")]
    public bool debug;


    private void Awake()
    {
        Init();
        DontDestroyOnLoad(this.gameObject);
    }


    private void Start()
    {
        CheckOtherManagers();
    }
    
    private void CheckOtherManagers()
    {
        if (debug) {
            if (CamMaster.current == null) Debug.LogWarning("No Camera Master was found, Cam Transistions will not work");
            if (ProgressionMaster.current == null) Debug.LogWarning("No Progression Master was found, Progression based Events will not work");
            if (DiscoverableMaster.current == null) Debug.LogWarning("No Discovery Master was found, discovery and Note book will not work");
            if (QuickTimeMaster.current == null) Debug.LogWarning("No Quicktime Master was found, QuickTime events will not work");
        }
    }


    private void Update()
    {
        if (state == State._posessing && (QuickTimeMaster.current == null || QuickTimeMaster.current.state != QuickTimeMaster.State.inprogress))
        {
            _time -= Time.deltaTime;
            if(_time <= 0)
            {
                PosessionStop();
            }
        }
        StateCooldown();
    }

    private void StateCooldown()
    {
        if(nextState != State._transition && _transitionTime >= 0)
        {
            _transitionTime -= Time.deltaTime;
        }else if( nextState != State._transition && _transitionTime < 0)
        {
            state = nextState;
            nextState = State._transition;
        }
    }

    private void SetState(State pstate)
    {
        
        if (nextState == State._transition)
        {
           // Debug.Log($"Set State");
            state = State._transition;
            nextState = pstate;
            _transitionTime = transitionTime;
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
            if(debug) Debug.LogWarning("No player could be found");
            return;
        } else if (players.Length > 1)
        {
            if (debug) Debug.LogWarning($" Only one player per seene can exists Players found: {players.Length}");
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
            if (debug) Debug.LogWarning("Multible GameMasters detected, Destroying this", this);
            Destroy(this.gameObject);
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
        if (debug) Debug.Log($"Started Poession");
        if (state == State._base)
        {
            _time = posession.posessionTime;
            SetState(State._posessing);
            CamMaster.current.SetCam(posession.gameObject);
            if (onPosessionStart != null)
            {
                onPosessionStart(posession);
            }
        }
    }
    public event Action onPosessionStop;
    public void PosessionStop()
    {
        if (debug) Debug.Log("Posession Stop");
        if (state == State._posessing)
        {
            state = State._base;
            if (onPosessionStop != null)
            {
                onPosessionStop();
                CamMaster.current.SetCam(CamMaster.current.playerConnection);
            }
        }
    }
    public event Action<GameObject> onInspectionStart;
    public void InspectionStart (GameObject obj)
    {
        //ToDo a is interacatbe check
        if (state == State._base)
        {
            SetState(State._inspecting);
            CamMaster.current.SetCam(obj);       
        }
        if(onInspectionStart != null) 
        {
            onInspectionStart(obj);
        }
    }
    public event Action<GameObject> onInpsectionStop;

    public void InspectionStop()
    {
        if (debug) Debug.Log("InpectionStoped");
        if (state == State._inspecting)
        {
            SetState(State._base);
            if (onInpsectionStop != null)
            {
                onInpsectionStop(CamMaster.current.connections[CamMaster.current.currentConnectionIndex].target);
            }
            CamMaster.current.SetCam(CamMaster.current.playerConnection.target);
        }
    }



    #endregion
    #region Overides
    public override void Init()
    {
        InitiateGameMaster();
        FindPlayer();
        GetPosessions();
        InitAllMasters();
    }
    //Inits all Masters
    public void InitAllMasters()
    {
        Master[] masters = GetComponents<Master>();

        foreach (Master m in masters)
        {
            if (m != this)
                m.Init();
        }

    }
    //Calls all masters and loads reverences that are needed on a scean to sean basis
    public override void ScenneStart()
    {
        FindPlayer();
        GetPosessions();
        Master[] masters = GetComponents<Master>();

        foreach (Master m in masters)
        {
            if (m != this)
                m.ScenneStart();
        }
    }
    

    #endregion

}
