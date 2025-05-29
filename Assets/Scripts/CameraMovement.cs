using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;

    [SerializeField]public GameObject HighscoreCar;
    [SerializeField]public Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField]public float smoothTime = 0.25f;
    [SerializeField] public float rotateTime = 0.75f;
    [SerializeField]private Transform target;

    public bool IsRotate = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow(HighscoreCar);
        //if the car in the front touches the trigger then it rotates
        if (IsRotate)
        {
            Quaternion currentRotation = gameObject.transform.rotation;
            //i don't get quaternions at all :(
            Quaternion targetLocation = Quaternion.Euler(0, 0, 90);

            gameObject.transform.rotation = Quaternion.Slerp(currentRotation, targetLocation, rotateTime * Time.deltaTime);
            Debug.Log("currentRotation:" + currentRotation.eulerAngles);
            if(currentRotation.eulerAngles == targetLocation.eulerAngles)
            {
                Debug.Log("finished rotating");
                IsRotate = false;
            }
        }
        //gameObject.transform.Rotate(new Vector3(0, 0, TurnDegrees));
    }

    public void CameraFollow(GameObject player)
    {
        //gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        //camera follows player car and smooths it based on smooth time
        Vector3 targetPostion = player.transform.position + offset;
        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, targetPostion, ref velocity, smoothTime);
    }

    //should just rotate the camera when it hits the trigger by how many degrees you enter for now
    public void CameraTurn(float TurnDegrees, GameObject Car)
    {
        //if the car in the front touches the trigger then it rotates
        if (Car == HighscoreCar)
        {
            Quaternion currentRotation = gameObject.transform.rotation;
            //i don't get quaternions at all :(
            Quaternion targetLocation = Quaternion.Euler(0, 0, TurnDegrees);

            gameObject.transform.rotation = Quaternion.Slerp(currentRotation, targetLocation, rotateTime);
            //gameObject.transform.Rotate(new Vector3(0, 0, TurnDegrees));

        }
    }

    public void ScreenShake()
    {

    }
}
