using UnityEngine;


public class PathProgress : MonoBehaviour
{

    public PathManager pathManager;


    private float lastProgress = -1f;



    private void Update()
    {

        if (pathManager == null)
            return;


        float progress =
            pathManager.GetProgress();



        if (progress != lastProgress)
        {

            lastProgress = progress;


            Debug.Log(
                "Path Progress: "
                +
                progress.ToString("F1")
                +
                "%"
            );

        }

    }




    public int GetCompletedTargets()
    {

        if (pathManager == null)
            return 0;


        return pathManager.completedTargets;

    }




    public int GetTotalTargets()
    {

        if (pathManager == null)
            return 0;


        return pathManager.targets.Length;

    }




    public float GetPathPercentage()
    {

        if (pathManager == null)
            return 0;


        return pathManager.GetProgress();

    }

}