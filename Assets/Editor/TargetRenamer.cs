using UnityEngine;
using UnityEditor;

public class TargetRenamer : MonoBehaviour
{
    [MenuItem("Tools/Auto-Number and Assign Targets")]
    public static void RenameAndAssign()
    {
        GameObject parent = Selection.activeGameObject;
        
        if (parent == null || parent.name != "Targets")
        {
            Debug.LogWarning("Please select the 'Targets' parent folder in the Hierarchy first!");
            return;
        }

        // 1. Find the PathManager in the scene
        PathManager pathManager = FindObjectOfType<PathManager>();
        if (pathManager == null)
        {
            Debug.LogError("Could not find the PathManager in the scene!");
            return;
        }

        // Prepare the array to hold exactly 100 targets
        Target[] newTargetArray = new Target[parent.transform.childCount];

        // 2. Loop through and process everything
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            
            // Rename the visual object
            child.name = "Target - " + i.ToString("D2"); 

            // Update the CSV ID and add it to our array
            Target targetScript = child.GetComponent<Target>();
            if (targetScript != null)
            {
                targetScript.targetID = i;
                newTargetArray[i] = targetScript; // Add to the PathManager list
                EditorUtility.SetDirty(targetScript);
            }
        }
        
        // 3. Inject the finished array directly into the PathManager
        pathManager.targets = newTargetArray;
        EditorUtility.SetDirty(pathManager);
        
        Debug.Log("Success! Renamed " + parent.transform.childCount + " targets and assigned them to the PathManager.");
    }
}
