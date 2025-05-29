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
    private Quaternion currentRotation;
    private Quaternion targetLocation;
    private bool TargetRotationSet = false;

    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.7f;
    private float dampingSpeed = 1f;
    Vector3 InitialPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialPos = gameObject.transform.position;
        ScreenShake(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow(HighscoreCar);
        //if the car in the front touches the trigger then it rotates
        if (IsRotate)
        {
            currentRotation = gameObject.transform.rotation;
            gameObject.transform.rotation = Quaternion.Slerp(currentRotation, targetLocation, rotateTime * Time.deltaTime);
            Debug.Log("currentRotation:" + currentRotation.eulerAngles.z);
            Debug.Log("targetRotation:" + targetLocation.eulerAngles.z);
            if(currentRotation.eulerAngles.z >= targetLocation.eulerAngles.z - 10)
            {
                Debug.Log("finished rotating");
                IsRotate = false;
                TargetRotationSet = false;
            }
        }

        if(shakeDuration > 0)
        {
            gameObject.transform.localPosition += InitialPos + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            //gameObject.transform.localPosition = InitialPos;
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
            //Quaternion currentRotation = gameObject.transform.rotation;
            //i don't get quaternions at all :(
            //Quaternion targetLocation = Quaternion.Euler(0, 0, TurnDegrees);

            //gameObject.transform.rotation = Quaternion.Slerp(currentRotation, targetLocation, rotateTime);
            //gameObject.transform.Rotate(new Vector3(0, 0, TurnDegrees));

            //i don't get quaternions at all :(
            if (TargetRotationSet == false)
            {
                currentRotation = gameObject.transform.rotation;
                targetLocation = Quaternion.Euler(0, 0, (currentRotation.eulerAngles.z + TurnDegrees));
                TargetRotationSet = true;
                IsRotate = true;
            }
        }
    }

    public void ScreenShake(float duration)
    {
        shakeDuration = duration;
    }
}
