using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    //Initial setup
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    // Behavior
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    // Utilities 
    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }


    void Start()
    {
        // Math
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        // Instantiate flock
        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                //Object
                agentPrefab,
                //Random point to instantiate
                Random.insideUnitCircle * startingCount * AgentDensity,
                //Rotation of the object
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                //Parent = the flock's transform
                transform
                );
            newAgent.name = "Agent " + 1;
            agents.Add(newAgent);
        }
    }


    void Update()
    {
        //iTERATING TROUGH THE AGENTS
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            // Calculate the move
            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            //Check if we have not surpassed the max speed
            if(move.sqrMagnitude > squareMaxSpeed)
            {
                // Reset speed
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }
    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius); // IMAGINARY CIRCLE IN SPACE AT A POINT IN THIS RADIUS AND SEE THE COLLIDERS
        // Grab the colliders and add to the list
        foreach (Collider2D c in contextColliders)
        {
            if(c != agent.AgentCollider) // Verify is not the object we ar working with
            {
                context.Add(c.transform);
            }

        }
        return context;
    }
}
