using UnityEngine;
using UnityEngine.AI;

public class Patal : MonoBehaviour
{
    public float patrolRange;

    public Vector3 GetRandomPositionWithinRange()
    {
        for (int i = 0; i < 2; i++) // Try up to 30 times to find a valid point
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
            randomDirection.y = 0; // Keep on same horizontal plane
            Vector3 candidate = transform.position + randomDirection;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(candidate, out hit, 1.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        // Fallback: return current position if no valid point found
        return transform.position;
    }
}

