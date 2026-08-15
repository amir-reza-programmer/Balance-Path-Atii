using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float radius = 0.25f; // Size of the penalty zone
    public bool isHit = false;

    // Checks if the foot position overlaps the obstacle 
    public bool CheckFootPosition(Vector3 footPosition)
    {
        // Convert 3D world position to flat 2D math (ignoring height)
        Vector2 obstaclePos = new Vector2(transform.position.x, transform.position.z);
        Vector2 footPos2D = new Vector2(footPosition.x, footPosition.z);
        
        return Vector2.Distance(obstaclePos, footPos2D) <= radius;
    }
}