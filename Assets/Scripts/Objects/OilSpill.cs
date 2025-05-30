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
            GameObject.Find("AudioManager").GetComponent<AudioManager>().SFXSource.PlayOneShot(GameObject.Find("AudioManager").GetComponent<AudioManager>().OilSlip);
            GameObject.Find("AudioManager").GetComponent<AudioManager>().SFXSource.PlayOneShot(GameObject.Find("AudioManager").GetComponent<AudioManager>().Screech);
            float torque = Random.Range(-spinForce, spinForce);
            CarControllerRB controller = rb.GetComponent<CarControllerRB>();
            if (controller != null)
            {
                controller.ApplySpin(torque);
            }
            else
            {
                rb.AddTorque(torque, ForceMode2D.Impulse); // fallback
            }
            Debug.Log($"Oil spill hit! Spinning player with torque: {torque}");

            ScoreManager sm = FindFirstObjectByType<ScoreManager>();
            int index = PlayerUtils.GetPlayerIndex(other.gameObject);
            if (sm != null && index >= 0)
            {
                sm.DeductScore(index, 2);
            }

        }


        if (destroyOnUse)
        {
            Destroy(gameObject);
        }
    }
}
