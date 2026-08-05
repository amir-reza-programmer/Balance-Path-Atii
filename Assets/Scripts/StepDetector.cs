using UnityEngine;


public class StepDetector : MonoBehaviour
{

    // Reference to CSV Logger
    public CSVLogger csvLogger;



    // Minimum movement distance for step detection
    public float stepThreshold = 0.15f;



    // Minimum time between two steps
    public float stepCooldown = 0.5f;



    // Previous foot positions
    private Vector3 previousLeftFoot;

    private Vector3 previousRightFoot;



    // Step counter
    private int stepCounter = 0;



    // Last detected step time
    private float lastStepTime;





    private void Start()
    {

        if (SensorFusion.Instance != null)
        {

            SensorData data =
                SensorFusion.Instance.sensorData;



            previousLeftFoot =
                data.leftFootPosition;


            previousRightFoot =
                data.rightFootPosition;

        }

    }





    private void Update()
    {

        if (SensorFusion.Instance == null)
            return;



        DetectStep();

    }







    private void DetectStep()
    {

        SensorData data =
            SensorFusion.Instance.sensorData;



        float leftMovement =
            Vector3.Distance(
                data.leftFootPosition,
                previousLeftFoot
            );



        float rightMovement =
            Vector3.Distance(
                data.rightFootPosition,
                previousRightFoot
            );




        // Avoid multiple detections
        if (Time.time - lastStepTime < stepCooldown)
        {
            UpdatePreviousPositions();
            return;
        }






        // Left foot step detected
        if (leftMovement > stepThreshold &&
           leftMovement > rightMovement)
        {

            RegisterStep(true);

        }




        // Right foot step detected
        else if (rightMovement > stepThreshold)
        {

            RegisterStep(false);

        }



        UpdatePreviousPositions();

    }








    private void RegisterStep(bool leftFoot)
    {

        SensorData data =
            SensorFusion.Instance.sensorData;



        stepCounter++;



        // Save step information

        data.stepNumber =
            stepCounter;



        data.leftFootStep =
            leftFoot;



        data.rightFootStep =
            !leftFoot;



        data.timeStamp =
            Time.time;



        lastStepTime =
            Time.time;





        Debug.Log(
            "Step "
            +
            stepCounter
            +
            " : "
            +
            (leftFoot ? "Left" : "Right")
        );





        // Save to CSV

        if (csvLogger != null)
        {

            csvLogger.SaveStepData();

        }

    }








    private void UpdatePreviousPositions()
    {

        SensorData data =
            SensorFusion.Instance.sensorData;



        previousLeftFoot =
            data.leftFootPosition;



        previousRightFoot =
            data.rightFootPosition;

    }

}