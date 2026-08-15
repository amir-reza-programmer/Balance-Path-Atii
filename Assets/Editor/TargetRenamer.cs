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

        
        PathManager pathManager = FindObjectOfType<PathManager>();
        if (pathManager == null)
        {
            Debug.LogError("Could not find the PathManager in the scene!");
            return;
        }

        
        Target[] newTargetArray = new Target[parent.transform.childCount];

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Transform child = parent.transform.GetChild(i);
            
            // rename
            child.name = "Target - " + i.ToString("D2"); 

            Target targetScript = child.GetComponent<Target>();
            if (targetScript != null)
            {
                targetScript.targetID = i;
                newTargetArray[i] = targetScript;
                EditorUtility.SetDirty(targetScript);
            }
        }
        
        // inject directly into the PathManager
        pathManager.targets = newTargetArray;
        EditorUtility.SetDirty(pathManager);
        
        Debug.Log("Success! Renamed " + parent.transform.childCount + " targets and assigned them to the PathManager.");
    }
}
