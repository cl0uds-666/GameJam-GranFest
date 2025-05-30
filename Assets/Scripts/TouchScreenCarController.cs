using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchScreenCarController : MonoBehaviour
{
    // Defines the touch zones (left/right) for a specific car
    [System.Serializable]
    public class CarTouchZone
    {
        public CarControllerRB car;       // The car this zone controls
        public Rect leftZone;             // Normalized screen area for turn left
        public Rect rightZone;            // Normalized screen area for turn right
    }

    [SerializeField] private CarTouchZone[] carZones; // Array of all car control zones
    [SerializeField] private bool showDebugZones = true; // Toggle to visually draw the touch zones
    private Vector2 screenSize; // Stores current screen resolution

    [Header("Idle Timeout")]
    [SerializeField] private float idleTimeout = 15f;
    [SerializeField] private IdleScreenManager idleScreenManager;
    private float lastInputTime;

    void Awake()
    {
        EnhancedTouchSupport.Enable(); // Enables Enhanced Touch input system
        screenSize = new Vector2(Screen.width, Screen.height); // Cache screen size for conversion
        lastInputTime = Time.time; // Set initial input time
    }

    void Update()
    {
        bool hasInput = false;

        // For each car, check if a touch is in its zone and update turn direction
        foreach (var zone in carZones)
        {
            bool isTurning = false;

            // Loop through all active touches on screen
            foreach (var touch in Touch.activeTouches)
            {
                Vector2 touchPos = touch.screenPosition;

                if (ScreenZoneContains(zone.leftZone, touchPos))
                {
                    zone.car.TurnLeft();
                    isTurning = true;
                    hasInput = true;
                    break;
                }
                else if (ScreenZoneContains(zone.rightZone, touchPos))
                {
                    zone.car.TurnRight();
                    isTurning = true;
                    hasInput = true;
                    break;
                }
            }

            // Mouse input support
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Input.mousePosition;
                if (ScreenZoneContains(zone.leftZone, mousePos))
                {
                    zone.car.TurnLeft();
                    isTurning = true;
                    hasInput = true;
                    break;
                }
                else if (ScreenZoneContains(zone.rightZone, mousePos))
                {
                    zone.car.TurnRight();
                    isTurning = true;
                    hasInput = true;
                    break;
                }
            }

            // If no touches in either zone, stop turning
            if (!isTurning)
            {
                zone.car.StopTurning();
            }
        }

        // Update last input time or check idle timeout
        if (hasInput)
        {
            lastInputTime = Time.time;
        }
        else if (Time.time - lastInputTime >= idleTimeout)
        {
            Debug.Log("Idle timeout reached — returning to idle screen.");
            if (idleScreenManager != null)
            {
                idleScreenManager.ReturnToIdle();
            }
        }
    }

    // Converts a normalized Rect (0-1 space) to screen space and checks if a touch is inside it
    private bool ScreenZoneContains(Rect normalizedZone, Vector2 touchPos)
    {
        Rect pixelZone = new Rect(
            normalizedZone.x * screenSize.x,
            normalizedZone.y * screenSize.y,
            normalizedZone.width * screenSize.x,
            normalizedZone.height * screenSize.y
        );

        return pixelZone.Contains(touchPos);
    }

    // Draw visual debug rectangles in the scene view using OnGUI
    void OnGUI()
    {
        if (!showDebugZones) return;

        foreach (var zone in carZones)
        {
            DrawZone(zone.leftZone, Color.red);   // Left zones in red
            DrawZone(zone.rightZone, Color.green); // Right zones in green
        }
    }

    // Helper method to draw a translucent colored box for a touch zone
    private void DrawZone(Rect normalizedZone, Color color)
    {
        Rect pixelZone = new Rect(
            normalizedZone.x * screenSize.x,
            normalizedZone.y * screenSize.y,
            normalizedZone.width * screenSize.x,
            normalizedZone.height * screenSize.y
        );

        Color oldColor = GUI.color;
        Color translucentColor = new Color(color.r, color.g, color.b, 0.25f);
        GUI.color = translucentColor;
        GUI.DrawTexture(pixelZone, Texture2D.whiteTexture);
        GUI.color = oldColor;
    }
}
