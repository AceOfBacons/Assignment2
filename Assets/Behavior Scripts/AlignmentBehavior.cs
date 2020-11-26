using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //If no neighbors, return maintain current alignment
        if (context.Count == 0)
        {
            return agent.transform.up;
        }
        //add all points together and average
        Vector2 alignmentMove = Vector2.zero;
        foreach (Transform item in context)
        {
            alignmentMove += (Vector2)item.transform.up;

        }
        // middle point between the neighbors
        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
