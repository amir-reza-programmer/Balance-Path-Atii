using UnityEngine;


public class Target : MonoBehaviour
{

    // Target identification number
    public int targetID;


    // Acceptable target radius
    public float radius = 0.20f;


    private Renderer targetRenderer;

    private AudioSource audioSource;

    private void Awake()
    {
        targetRenderer = GetComponentInChildren<Renderer>();
        audioSource = GetComponent<AudioSource>();

        if (targetRenderer == null)
        {
            Debug.LogWarning(
                "Renderer not found for Target: " + gameObject.name
            );
        }
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource not found for Target: " + gameObject.name);
        }
    }




    private void Start()
    {
        SetDefault();
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(
            transform.position,
            radius
        );
    }




    public bool CheckFootPosition(Vector3 footPosition)
    {
        Vector2 targetPosition =
            new Vector2(
                transform.position.x,
                transform.position.z
            );


        Vector2 footPosition2D =
            new Vector2(
                footPosition.x,
                footPosition.z
            );


        float distance =
            Vector2.Distance(
                targetPosition,
                footPosition2D
            );


        return distance <= radius;
    }




    // Set initial target color
    public void SetDefault()
    {
        ChangeColor(Color.red);
    }




    // Set active target color
    public void SetCurrent()
    {
        ChangeColor(Color.yellow);
    }




    // Set completed target color
    public void SetCompleted()
    {
        ChangeColor(Color.green);
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }




    private void ChangeColor(Color color)
    {
        if (targetRenderer != null)
        {
            targetRenderer.material.color = color;
        }
    }

}