using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;

    [SerializeField]public GameObject Player1;
    [SerializeField]public Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField]public float smoothTime = 0.25f;
    [SerializeField]private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow(Player1);
    }

    //when colliding with a trigger it rotates, might use rotate around to rotate around the trigger's centre
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CameraTurn(90);
        Debug.Log("COLLIDING");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CameraTurn(90);
        Debug.Log("COLLIDING");
    }

    public void CameraFollow(GameObject player)
    {
        //gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        //camera follows player car and smooths it based on smooth time
        Vector3 targetPostion = player.transform.position + offset;
        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, targetPostion, ref velocity, smoothTime);
    }

    //should just rotate the camera when it hits the trigger by how many degrees you enter for now
    public void CameraTurn(float TurnDegrees)
    {
        gameObject.transform.Rotate(new Vector3(0, 0 , TurnDegrees));
    }

    public void ScreenShake()
    {

    }
}
