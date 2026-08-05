using UnityEngine;


public class TargetDetector : MonoBehaviour
{

    // Reference to PathManager
    public PathManager pathManager;


    // How often target checking happens
    public float checkInterval = 0.1f;


    private float timer;



    private void Update()
    {

        timer += Time.deltaTime;


        if (timer >= checkInterval)
        {

            timer = 0;

            CheckTarget();

        }

    }





    private void CheckTarget()
    {

        // Check required references
        if (pathManager == null)
            return;


        if (SensorFusion.Instance == null)
            return;



        // Get current target
        Target currentTarget =
            pathManager.GetCurrentTarget();



        if (currentTarget == null)
            return;



        // Get sensor data
        SensorData data =
            SensorFusion.Instance.sensorData;



        // Check left foot
        bool leftCorrect =
            currentTarget.CheckFootPosition(
                data.leftFootPosition
            );



        // Check right foot
        bool rightCorrect =
            currentTarget.CheckFootPosition(
                data.rightFootPosition
            );



        bool correctStep =
            leftCorrect || rightCorrect;



        // Calculate distance from nearest foot
        float distance =
            CalculateMinimumDistance(
                currentTarget,
                data
            );



        // Save target information
        data.targetID =
            currentTarget.targetID;


        data.correctStep =
            correctStep;


        data.targetDistance =
            distance;




        // Correct step detected
        if (correctStep)
        {

            // Save completed target information
            data.completedTargets =
                pathManager.completedTargets + 1;



            Debug.Log(
                "Correct step on Target: "
                +
                currentTarget.targetID
            );



            // Move to next target
            pathManager.CompleteCurrentTarget();

        }

    }





    private float CalculateMinimumDistance(
        Target target,
        SensorData data)
    {

        float leftDistance =
            Vector3.Distance(
                target.transform.position,
                data.leftFootPosition
            );



        float rightDistance =
            Vector3.Distance(
                target.transform.position,
                data.rightFootPosition
            );



        return Mathf.Min(
            leftDistance,
            rightDistance
        );

    }

}