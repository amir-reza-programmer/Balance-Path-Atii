using UnityEngine;


[System.Serializable]
public class SensorData
{

    // =========================
    // Headset SLAM Tracking Data
    // =========================

    public Vector3 headPosition;

    public Quaternion headRotation;



    // =========================
    // Estimated Foot Tracking
    // =========================

    public Vector3 leftFootPosition;

    public Vector3 rightFootPosition;


    public Quaternion leftFootRotation;

    public Quaternion rightFootRotation;



    // =========================
    // Estimated IMU Data
    // =========================

    public Vector3 acceleration;

    public Vector3 gyroscope;



    // =========================
    // Time Information
    // =========================

    public float timeStamp;



    // =========================
    // Step Information
    // =========================

    public int stepNumber;

    public bool leftFootStep;

    public bool rightFootStep;



    // =========================
    // Target Information
    // =========================

    public int targetID;

    public bool correctStep;

    public float targetDistance;



    // =========================
    // Path Information
    // =========================

    public int completedTargets;

    public int currentTargetID;

    public int totalTargets;

    public float pathProgress;

    public float traveledDistance;



    // =========================
    // Session Information
    // =========================

    public int trialID;

    public string participantID;

}