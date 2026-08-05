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

}