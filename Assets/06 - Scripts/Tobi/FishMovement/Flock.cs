using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [HideInInspector]
    public Transform player;

    [SerializeField]
    private string playerTag = "Player";
    
    // Exposed Variables
    [Header("FlockSettings")]
    [SerializeField]
    [Tooltip("This switches the prosess to designated place")]
    private CalcMode calculationMode;

    [SerializeField]
    private FlockAgent prefab;

    [SerializeField]
    private FlockBehavior flockBehavior;

    [SerializeField]
    [Range(0, 1000)]
    private int startingCount = 250;

    [SerializeField]
    [Range(1f, 100f)]
    private float driveFactor = 10f;

    [SerializeField]
    [Range(1f, 100f)]
    private float maxSpeed = 10f;

    [SerializeField]
    [Range(1f, 10f)]
    private float neighbourRadius = 1.5f;

    [SerializeField]
    [Range(0f, 1f)]
    private float avoidanceRadiusMultiplier = 0.5f;
    [SerializeField]
    [Range(0f, 1f)]
    [Tooltip("The lower the closer the agens spawn together")]
    private float agentDensity = 0.8f;

    [Header("RayCast variables")]
    [SerializeField]
    private int numberOffRays = 15;
    [SerializeField]
    private float turnfraction = 1.618034f;
    [SerializeField]
    private float viewAngle = 160;
    [SerializeField]
    private bool debugRays = false;
    
    //private float dist = 1;

    // Hidden variables
    private List<FlockAgent> agents = new List<FlockAgent>();
    [HideInInspector]
    public List<Vector3> plottedPoints;

    //Optimisation variabels
    private float squareMaxSpeed;
    private float squareNeighborRadius;
    private float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private enum CalcMode { CPU, GPU, None };

    // Start is called before the first frame update
    void Start()
    {
        InitReverences();

        //Calculate MathHelpers
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = neighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        SpawnFish();

    }

    //Check if the neede static variabels are assinged / generated and assing / generate them
    private void InitReverences()
    {
        if (plottedPoints == null || plottedPoints.Count == 0) CalcRays();
        else Debug.Log($"Rays were allready Calculated  Ray amount = { plottedPoints.Count}", this);

        if(player == null)
        {
            //get all objects taged as player
            GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);
            //Check if there are more or less than one object as player taged
            if ( players == null || players.Length > 1 || players.Length == 0)
            {
                Debug.LogWarning($"Problem with identifying the player, pleas tag one object as player. \n Current Playercount : {players.Length}");
            }
            else
            {
                player = players[0].GetComponent<Transform>();
            }
        }
        else
        {
            Debug.Log("Player was already assinged", player);
        }

    }

    private void SpawnFish()
    {
        //Instanciate all flock Agents
        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                prefab,
                transform.position + (Random.insideUnitSphere * startingCount * agentDensity),
                Quaternion.Euler(Vector3.up * Random.Range(0f, 100f)),
                transform);
            newAgent.name = $"Agent{i}";
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (debugRays) DrawDebuggPlottedRays();

        switch (calculationMode)
        {
            case CalcMode.CPU:
                CalcCPU();
                break;
            case CalcMode.GPU:
                CalcGPU();
                break;
            case CalcMode.None:
                break;
            default:
                break;
        }
    }

    private void CalcGPU()
    {
        Debug.LogError("GPU Calculation is not implemeted yet", this);
    }

    private void CalcCPU()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> ctx = GetNearbyObjects(agent);


            Vector3 move = flockBehavior.CalculateMove(agent, ctx, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    //Draws Rays in the sceen window for debug
    private void DrawDebuggPlottedRays()
    {
        foreach (Vector3 point in plottedPoints)
        {
            Debug.DrawRay(transform.position, point);
        }
    }

    //Returns a list of all neerby objects without the origanal agent
    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> ctx = new List<Transform>();
        Collider[] ctxColliderd = Physics.OverlapSphere(agent.transform.position, neighbourRadius);
        foreach (Collider c in ctxColliderd)
        {
            if (c != agent.AgentCollider) ctx.Add(c.transform);
            //if (c.CompareTag("Player")) Debug.Log("Player added");
        }
        return ctx;
    }
    //Calculates all Rays depenting on the number and turnfraction
    private void CalcRays()
    {
        plottedPoints = new List<Vector3>();


        for (int i = 0; i < numberOffRays; i++)
        {
            float t = i / (numberOffRays - 1f);
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = 2 * Mathf.PI * turnfraction * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);
            PlotRayTest(new Vector3(x, y, z));
        }
        plottedPoints.RemoveAt(0);
        SortPlotedPoints();
    }

    //Sorts the Plotted points list (using bublesort)
    private void SortPlotedPoints()
    {
        if (plottedPoints.Count < 2)
        {
            Debug.LogWarning("Plotted poits are less than 2");
            return;
        }
        for (int j = plottedPoints.Count - 1; j > 0; j--)
        {
            for (int i = 0; i < j; i++)
            {
                if (Mathf.Abs(Vector3.Angle(plottedPoints[i], transform.forward)) > Mathf.Abs(Vector3.Angle(plottedPoints[i + 1], transform.forward)))
                {
                    SwapPoints(plottedPoints, i, i + 1);
                }
            }
        }
        if (debugRays) WriteSortToConsole();
    }

    //swaps 2 point in a given list 
    private void SwapPoints(List<Vector3> dataset, int indexM, int indexN)
    {
        Vector3 tempVec;
        tempVec = dataset[indexM];
        dataset[indexM] = dataset[indexN];
        dataset[indexN] = tempVec;
    }

    //Writes a list of all the plotted angles to the console
    private void WriteSortToConsole()
    {
        string debugginfo = "Calculating Rays: \n";
        for (int i = 0; i < plottedPoints.Count; i++)
        {
            debugginfo += $"index = {i} Angle = {Mathf.Abs(Vector3.Angle(plottedPoints[i], transform.forward))} \n ";
        }
        Debug.Log(debugginfo, this);
    }


    //Adds The point to plotted points if it falls in the field of view
    void PlotRayTest(Vector3 point)
    {
        if (Mathf.Abs(Vector3.Angle(point, transform.forward)) < viewAngle)
        {
            plottedPoints.Add(point);
        }
    }

}