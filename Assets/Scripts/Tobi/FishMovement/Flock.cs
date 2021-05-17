using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    // Exposed Variables
    [SerializeField]
    private FlockAgent prefab;

    [SerializeField]
    private FlockBehavior flockBehavior;

    [SerializeField]
    [Range(1,500)]
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
    [Range(0f,1f)]
    private float agentDensity = 0.8f;


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
        //Calculate MathHelpers
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighbourRaidus * neighbourRaidus;
        squareAvoidanceRadius = neighbourRaidus * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;


        //Instanciate all flock Agents
        for(int i = 0; i <startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                prefab,
                Random.insideUnitSphere * startingCount * agentDensity,
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
}
