using UnityEngine;


public class FootTracker : MonoBehaviour
{

    // Visual foot objects
    public Transform leftFoot;
    public Transform rightFoot;


    // Estimated distance values
    public float footHeightOffset = 1.2f;
    public float forwardOffset = 0.25f;
    public float sideOffset = 0.20f;



    private void Update()
    {

        if (SensorFusion.Instance == null)
            return;


        EstimateFootPositions();

    }




    private void EstimateFootPositions()
    {

        // Get sensor data from SensorFusion
        SensorData data =
            SensorFusion.Instance.sensorData;



        Vector3 headPosition =
            data.headPosition;


        Quaternion headRotation =
            data.headRotation;



        // Convert rotation to direction vectors
        Vector3 forwardDirection =
            headRotation * Vector3.forward;


        Vector3 rightDirection =
            headRotation * Vector3.right;



        // Estimate left foot position
        Vector3 leftOffset =
            (-rightDirection * sideOffset)
            -
            (Vector3.up * footHeightOffset)
            +
            (forwardDirection * forwardOffset);



        // Estimate right foot position
        Vector3 rightOffset =
            (rightDirection * sideOffset)
            -
            (Vector3.up * footHeightOffset)
            +
            (forwardDirection * forwardOffset);




        // Save estimated foot positions
        data.leftFootPosition =
            headPosition + leftOffset;


        data.rightFootPosition =
            headPosition + rightOffset;



        // Save estimated foot rotations
        data.leftFootRotation =
            headRotation;


        data.rightFootRotation =
            headRotation;




        // Update left foot visual object
        if (leftFoot != null)
        {
            leftFoot.position =
                data.leftFootPosition;

            leftFoot.rotation =
                data.leftFootRotation;
        }




        // Update right foot visual object
        if (rightFoot != null)
        {
            rightFoot.position =
                data.rightFootPosition;

            rightFoot.rotation =
                data.rightFootRotation;
        }

    }

}