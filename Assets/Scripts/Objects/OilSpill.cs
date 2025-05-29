using UnityEngine;

public class OilSpill : MonoBehaviour
{
    [SerializeField] private float spinForce = 300f;
    [SerializeField] private bool destroyOnUse = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb)
        {
            AudioManager audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            audio.SFXSource.PlayOneShot(audio.OilSlip);
            audio.SFXSource.PlayOneShot(audio.Screech);

            float torque = Random.Range(-spinForce, spinForce);
            Debug.Log($"Oil spill hit! Spinning player with torque: {torque}");

            // Try applying spin via CarControllerRB
            CarControllerRB controller = rb.GetComponent<CarControllerRB>();
            if (controller != null)
            {
                controller.ApplySpin(torque);
            }
            else
            {
                // If not present, try IdleAutoplay instead
                IdleAutoplay idleAutoplay = rb.GetComponent<IdleAutoplay>();
                if (idleAutoplay != null)
                {
                    rb.AddTorque(torque, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddTorque(torque, ForceMode2D.Impulse); // fallback
                }
            }
        }

        if (destroyOnUse)
        {
            Destroy(gameObject);
        }
    }
}
