using UnityEngine;
using System.Linq;

public class ObstacleManager : MonoBehaviour
{
    public Obstacle[] allObstacles;
    public int obstaclesToKeep = 10;

    void Start()
    {
        // 1. Shuffle the array randomly every time the game starts
        System.Random rnd = new System.Random();
        Obstacle[] shuffledObstacles = allObstacles.OrderBy(x => rnd.Next()).ToArray();

        // 2. Turn off 10 of them, leave 20 of them on!
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
