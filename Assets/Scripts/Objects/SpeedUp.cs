using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1.5f;     // Boost amount
    [SerializeField] private float boostDuration = 1.5f;       // How long the boost lasts
    [SerializeField] private bool destroyOnUse = false;        

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        CarControllerRB controller = other.GetComponent<CarControllerRB>();
        if (controller != null)
        {
            float boostedSpeed = controller.forwardSpeed * speedMultiplier;
            controller.ApplyTemporarySlow(boostedSpeed, boostDuration); // Using ApplyTemporarySlow as a generic speed override
            Debug.Log($"Speed boost! New speed: {boostedSpeed}");
        }

        if (destroyOnUse)
        {
            Destroy(gameObject);
        }
    }
}
