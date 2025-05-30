using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;

    [SerializeField]public GameObject HighscoreCar;
    [SerializeField]public Vector3 offset = new Vector3(0f, 0f, -10f);
    //[SerializeField]public float smoothTime = 0.25f;
	[SerializeField][Range(0f, 1f)] public float smoothFactor = 0.1f; // Lerp factor (closer to 1 = snappier)
	
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
		
		//if rotating boolean triggered, rotates to target rotation with lerp
        if (IsRotate)
        {
            currentRotation = gameObject.transform.rotation;
            gameObject.transform.rotation = Quaternion.Slerp(currentRotation, targetLocation, rotateTime * Time.deltaTime);
            //Debug.Log("currentRotation:" + currentRotation.eulerAngles.z);
            //Debug.Log("targetRotation:" + targetLocation.eulerAngles.z);
			
			//just checking to see if rotation is done
            if(currentRotation.eulerAngles.z >= targetLocation.eulerAngles.z - 10)
            {
                Debug.Log("finished rotating");
                IsRotate = false;
                TargetRotationSet = false;
            }
        }
		
		//keeps shakin' till lil timer is done
        if(shakeDuration > 0)
        {
			//adding random shake onto the camera's current position
            gameObject.transform.localPosition += InitialPos + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
        }
    }

    public void CameraFollow(GameObject player)
    {
		Vector3 targetPosition = player.transform.position + offset;

        // Lerp towards the target position with smoothFactor controlling the blend
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothFactor);
    }

    //should just rotate the camera when it hits the trigger by how many degrees you enter for now
    public void CameraTurn(float TurnDegrees, GameObject Car)
    {
        //if the car in the front touches the trigger then it rotates
        if (Car == HighscoreCar)
        {
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
