using UnityEngine;


public class SensorFusion : MonoBehaviour
{

    public static SensorFusion Instance;


    // VR headset camera
    public Transform headTransform;



    // Main sensor data container
    public SensorData sensorData;



    // Estimated foot offsets from headset
    public Vector3 leftFootOffset =
        new Vector3(-0.15f, -1.0f, 0.05f);


    public Vector3 rightFootOffset =
        new Vector3(0.15f, -1.0f, 0.05f);



    // Previous headset position
    private Vector3 previousHeadPosition;



    // Estimated velocity
    private Vector3 velocity;



    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        sensorData = new SensorData();

    }





    private void Start()
    {

        if (headTransform == null)
        {
            Debug.LogError(
                "Head Transform is not assigned!"
            );

            return;
        }


        previousHeadPosition =
            headTransform.position;

    }





    private void Update()
    {

        UpdateHeadData();

        CalculateDistance();
        
        EstimateFootData();

        EstimateIMUData();

        UpdateTime();

    }






    private void UpdateHeadData()
    {

        sensorData.headPosition =
            headTransform.position;


        sensorData.headRotation =
            headTransform.rotation;


        Debug.Log(
            "Camera Pos: "
            + headTransform.position
            +
            " Rot: "
            +
            headTransform.rotation.eulerAngles
        );

    }







    private void EstimateFootData()
    {


        // Estimate left foot position
        sensorData.leftFootPosition =
            headTransform.position
            +
            headTransform.rotation
            *
            leftFootOffset;



        // Estimate right foot position
        sensorData.rightFootPosition =
            headTransform.position
            +
            headTransform.rotation
            *
            rightFootOffset;




        // Estimate foot rotation
        sensorData.leftFootRotation =
            headTransform.rotation;



        sensorData.rightFootRotation =
            headTransform.rotation;

    }








    private void EstimateIMUData()
    {


        // Calculate headset velocity
        velocity =
            (headTransform.position -
            previousHeadPosition)
            /
            Time.deltaTime;



        // Estimated acceleration
        sensorData.acceleration =
            velocity /
            Time.deltaTime;



        // Approximate gyroscope data
        sensorData.gyroscope =
            headTransform.rotation.eulerAngles;



        previousHeadPosition =
            headTransform.position;

    }








    private void UpdateTime()
    {

        sensorData.timeStamp =
            Time.time;

    }

    private void CalculateDistance()
    {
        float distanceThisFrame = Vector3.Distance(headTransform.position, previousHeadPosition);
        
        sensorData.traveledDistance += distanceThisFrame;
    }

}
