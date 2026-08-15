using UnityEngine;
using System.Linq;

public class ObstacleManager : MonoBehaviour
{
    public Obstacle[] allObstacles;
    public int obstaclesToKeep = 10;

    void Start()
    {
        // shuffle every time game starts
        System.Random rnd = new System.Random();
        Obstacle[] shuffledObstacles = allObstacles.OrderBy(x => rnd.Next()).ToArray();

        for (int i = 0; i < shuffledObstacles.Length; i++)
        {
            if (i >= obstaclesToKeep)
            {
                shuffledObstacles[i].gameObject.SetActive(false);
            }
            else
            {
                shuffledObstacles[i].gameObject.SetActive(true);
            }
        }
    }
}
