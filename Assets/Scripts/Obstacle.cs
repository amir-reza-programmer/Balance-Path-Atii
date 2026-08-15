using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float radius = 0.25f;
    public bool isHit = false;

    // if the foot position overlaps 
    public bool CheckFootPosition(Vector3 footPosition)
    {
        Vector2 obstaclePos = new Vector2(transform.position.x, transform.position.z);
        Vector2 footPos2D = new Vector2(footPosition.x, footPosition.z);
        
        return Vector2.Distance(obstaclePos, footPos2D) <= radius;
    }
}
