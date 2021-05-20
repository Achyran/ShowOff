using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    // Exposed Variables
    [Header("FlockSettings")]
    [SerializeField]
    bool enableCPUClac;

    [SerializeField]
    private FlockAgent prefab;

    [SerializeField]
    private FlockBehavior flockBehavior;

    [SerializeField]
    [Range(0, 1000)]
    private int startingCount = 250;

    [SerializeField]
    [Range(1f, 100f)]
    private float dirveFactor = 10f;

    [SerializeField]
    [Range(1f, 100f)]
    private float maxSpeed = 10f;

    [SerializeField]
    [Range(1f, 10f)]
    private float neighbourRaidus = 1.5f;

    [SerializeField]
    [Range(0f, 1f)]
    private float avoidanceRadiusMultiplier = 0.5f;
    [SerializeField]
    [Range(0f, 1f)]
    private float agentDensity = 0.8f;

    [Header("RayCast variables")]
    [SerializeField]
    private int numOffRays = 15;
    [SerializeField]
    private float turnfraction = 1.618034f;
    [SerializeField]
    private float highlightAngle = 160;
    [SerializeField]
    private bool debuggRays = false;
    [SerializeField]
    public static List<Vector3> plottedPoints;

    private float dist = 1;

    // Hidden variables
    private List<FlockAgent> agents = new List<FlockAgent>();

    //Optimisation variabels
    private float squareMaxSpeed;
    private float squareNeighborRadius;
    private float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        if (plottedPoints == null || plottedPoints.Count == 0) CalcRays();
        else Debug.Log($"Rays were allready Calculated  Ray amount = { plottedPoints.Count}", this);

        //Calculate MathHelpers
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighbourRaidus * neighbourRaidus;
        squareAvoidanceRadius = neighbourRaidus * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        SpawnFish();
        
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
        if (debuggRays) DrawDebuggPlottedRays();

        if(enableCPUClac)
        foreach(FlockAgent agent in agents)
        {
            List<Transform> ctx = GetNearbyObjects(agent);
            

            Vector3 move = flockBehavior.CalculateMove(agent, ctx, this);
            move *= dirveFactor;
            if(move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
        else
        {
            //Implement GPU Solution
        }
    }

    //Draws Rays in the sceen window for debug
    private void DrawDebuggPlottedRays()
    {
       foreach(Vector3 point in plottedPoints)
        {
            Debug.DrawRay(transform.position, point);
        }
    }

    //Returns a list of all neerby objects without the origanal agent
    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> ctx = new List<Transform>();
        Collider[] ctxColliderd = Physics.OverlapSphere(agent.transform.position, neighbourRaidus);
        foreach(Collider c in ctxColliderd)
        {
            if (c != agent.AgentCollider) ctx.Add(c.transform);
        }
        return ctx;
    }
    //Calculates all Rays depenting on the number and turnfraction
    private void CalcRays()
    {
        plottedPoints = new List<Vector3>();


        for (int i = 0; i < numOffRays; i++)
        {
            float t = i / (numOffRays - 1f);
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
        if(debuggRays) WriteSortToConsole();
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
        if (Mathf.Abs(Vector3.Angle(point, transform.forward)) < highlightAngle)
        {
            plottedPoints.Add(point);
        }
    }

}
