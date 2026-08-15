using UnityEngine;


public class PathManager : MonoBehaviour
{

    // Targets in walking order
    public Target[] targets;


    // Current target index
    public int currentTargetIndex = 0;


    // Completed target counter
    public int completedTargets = 0;



    private void Start()
    {
        InitializePath();
    }




    private void InitializePath()
    {
        Debug.Log("InitializePath method has started!");
        Debug.Log(targets.Length);
        for (int i = 0; i < targets.Length; i++)
        {

            if (targets[i] != null)
            {
                targets[i].gameObject.SetActive(true);

                targets[i].SetDefault();
            }

        }



        if (targets.Length > 0)
        {
            targets[currentTargetIndex].SetCurrent();
        }


        UpdateSensorData();

    }






    public Target GetCurrentTarget()
    {

        if (currentTargetIndex < targets.Length)
        {
            return targets[currentTargetIndex];
        }


        return null;

    }







    public void CompleteCurrentTarget()
    {

        if (currentTargetIndex >= targets.Length)
            return;



        targets[currentTargetIndex].SetCompleted();



        completedTargets++;



        currentTargetIndex++;





        if (currentTargetIndex < targets.Length)
        {

            targets[currentTargetIndex].SetCurrent();

        }
        else
        {

            FinishPath();

        }




        UpdateSensorData();

    }







    private void UpdateSensorData()
    {

        if (SensorFusion.Instance == null)
            return;



        SensorData data =
            SensorFusion.Instance.sensorData;



        data.completedTargets =
            completedTargets;



        data.currentTargetID =
            currentTargetIndex;



        data.totalTargets =
            targets.Length;



        if (targets.Length > 0)
        {

            data.pathProgress =
                ((float)completedTargets /
                targets.Length)
                * 100f;

        }

    }







    private void FinishPath()
    {

        Debug.Log(
            "Path Completed"
        );



        if (SensorFusion.Instance != null)
        {

            SensorFusion.Instance
            .sensorData.pathProgress = 100f;

        }

    }







    public float GetProgress()
    {

        if (targets.Length == 0)
            return 0f;



        return
            ((float)completedTargets /
            targets.Length)
            * 100f;

    }

private void OnDrawGizmos()
    {
        // Don't draw anything if the list is empty
        if (targets == null || targets.Length < 2)
            return;

        for (int i = 0; i < targets.Length - 1; i++)
        {
            if (targets[i] != null && targets[i + 1] != null)
            {
                // Calculate the distance between the current target and the next one
                float distance = Vector3.Distance(targets[i].transform.position, targets[i + 1].transform.position);

                // Check our strict distance rules
                if (distance > 0.5f)
                {
                    // Too far! Patient has to stretch.
                    Gizmos.color = Color.red; 
                }
                else if (distance < 0.35f) 
                {
                    // Too close! Patient has to shuffle.
                    Gizmos.color = Color.yellow; 
                }
                else
                {
                    // Goldilocks zone (0.35 to 0.5) - Perfect stride!
                    Gizmos.color = Color.green; 
                }

                // Draw the actual line in the Unity Scene window
                Gizmos.DrawLine(targets[i].transform.position, targets[i + 1].transform.position);
            }
        }
    }
}