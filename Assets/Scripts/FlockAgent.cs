using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    // Variables
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    void Start()
    {
        // Getting collider2d
        agentCollider = GetComponent<Collider2D>();
    }

    public void Move(Vector2 velocity)
    {
        // Turn agent to face direction its aiming
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime; // constant movement
    }
}
