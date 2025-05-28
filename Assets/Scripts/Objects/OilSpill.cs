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
        }


        if (destroyOnUse)
        {
            Destroy(gameObject);
        }
    }
}
