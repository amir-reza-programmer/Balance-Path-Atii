using UnityEngine;
using System.IO;
using System.Text;


public class CSVLogger : MonoBehaviour
{

    private string filePath;

    private StringBuilder csvContent;


    private void Start()
    {

        filePath =
            Application.persistentDataPath
            +
            "/VR_Gait_Data.csv";


        csvContent = new StringBuilder();



        csvContent.AppendLine(
            "Time," +
            "StepNumber," +
            "LeftFootStep," +
            "RightFootStep," +
            "HeadPosX," +
            "HeadPosY," +
            "HeadPosZ," +
            "LeftFootX," +
            "LeftFootY," +
            "LeftFootZ," +
            "RightFootX," +
            "RightFootY," +
            "RightFootZ," +
            "AccX," +
            "AccY," +
            "AccZ," +
            "GyroX," +
            "GyroY," +
            "GyroZ," +
            "TargetID," +
            "CorrectStep," +
            "TargetDistance," +
            "CompletedTargets," +
            "CurrentTargetID," +
            "TotalTargets," +
            "PathProgress," +
            "TraveledDistance"
        );


        File.WriteAllText(
            filePath,
            csvContent.ToString()
        );


        Debug.Log(
            "CSV Created: "
            +
            filePath
        );

    }







    public void SaveStepData()
    {

        if (SensorFusion.Instance == null)
            return;



        SensorData data =
            SensorFusion.Instance.sensorData;



        string line =
            data.timeStamp + "," +

            data.stepNumber + "," +

            data.leftFootStep + "," +

            data.rightFootStep + "," +


            data.headPosition.x + "," +
            data.headPosition.y + "," +
            data.headPosition.z + "," +


            data.leftFootPosition.x + "," +
            data.leftFootPosition.y + "," +
            data.leftFootPosition.z + "," +


            data.rightFootPosition.x + "," +
            data.rightFootPosition.y + "," +
            data.rightFootPosition.z + "," +


            data.acceleration.x + "," +
            data.acceleration.y + "," +
            data.acceleration.z + "," +


            data.gyroscope.x + "," +
            data.gyroscope.y + "," +
            data.gyroscope.z + "," +


            data.targetID + "," +

            data.correctStep + "," +

            data.targetDistance + "," +

            data.completedTargets + "," +

            data.currentTargetID + "," +

            data.totalTargets + "," +

            data.pathProgress + "," +

            data.traveledDistance;



        File.AppendAllText(
            filePath,
            line + "\n"
        );


        Debug.Log(
            "Step Data Saved"
        );

    }

}