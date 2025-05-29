using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] public GameObject HighscoreCar;
    [SerializeField] public Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField][Range(0f, 1f)] public float smoothFactor = 0.1f; // Lerp factor (closer to 1 = snappier)

    private Transform target;

    void Update()
    {
        ScoreManager sm = FindFirstObjectByType<ScoreManager>();
        if (sm != null)
        {
            GameObject newHighscoreCar = sm.GetHighscorePlayer();
            if (newHighscoreCar != null && newHighscoreCar != HighscoreCar)
            {
                HighscoreCar = newHighscoreCar;
            }
        }

        CameraFollow(HighscoreCar);
    }


    public void CameraFollow(GameObject player)
    {
        Vector3 targetPosition = player.transform.position + offset;

        // Lerp towards the target position with smoothFactor controlling the blend
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothFactor);
    }

    public void CameraTurn(float TurnDegrees, GameObject Car)
    {
        if (Car == HighscoreCar)
        {
            transform.Rotate(0, 0, TurnDegrees); // basic Z-axis rotation
        }
    }

    public void ScreenShake()
    {
        // Placeholder for future implementation
    }
}
