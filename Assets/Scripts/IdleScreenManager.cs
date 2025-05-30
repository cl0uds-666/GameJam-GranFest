using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class IdleScreenManager : MonoBehaviour
{
    [Header("Players")]
    public GameObject[] players;
    public Transform[] startPoints;

    [Header("UI")]
    public GameObject tapToPlayCanvas;
    public GameObject mainUICanvas;

    [Header("Traffic Lights")]
    public GameObject redLight;
    public GameObject redLight2;
    public GameObject redLight3;
    public GameObject greenLight;

    [Header("Camera")]
    public CameraMovement cameraMovement;
    public float idleSwitchInterval = 3f;

    private int currentCameraIndex = 0;
    private float nextCameraSwitchTime = 0f;


    private bool gameStarted = false;

    void Start()
    {
        tapToPlayCanvas.SetActive(true);
        mainUICanvas.SetActive(false);

        redLight.SetActive(false);
        redLight2.SetActive(false);
        redLight3.SetActive(false);
        greenLight.SetActive(false);

        // Enable IdleAutoplay, disable CarControllerRB
        foreach (GameObject player in players)
        {
            IdleAutoplay idleScript = player.GetComponent<IdleAutoplay>();
            if (idleScript != null)
                idleScript.enabled = true;

            CarControllerRB controller = player.GetComponent<CarControllerRB>();
            if (controller != null)
                controller.enabled = false;
        }
    }

    void Update()
    {
        if (!gameStarted)
        {
            HandleIdleCameraSwitch();

            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(BeginGameSequence());
            }
        }
    }

    private void HandleIdleCameraSwitch()
    {
        if (Time.time >= nextCameraSwitchTime && players.Length > 0)
        {
            currentCameraIndex = (currentCameraIndex + 1) % players.Length;
            if (cameraMovement != null)
            {
                cameraMovement.HighscoreCar = players[currentCameraIndex];
            }

            nextCameraSwitchTime = Time.time + idleSwitchInterval;
        }
    }

    public void ReturnToIdle()
    {
        SceneManager.LoadScene("MainScene"); 
    }


    private IEnumerator BeginGameSequence()
    {
        gameStarted = true;
        tapToPlayCanvas.SetActive(false);
        mainUICanvas.SetActive(true);

        // Reset players to start position/rotation and freeze them
        foreach (GameObject player in players)
        {
            player.transform.position = startPoints[System.Array.IndexOf(players, player)].position;
            player.transform.rotation = startPoints[System.Array.IndexOf(players, player)].rotation;

            // Disable IdleAutoplay
            IdleAutoplay idleScript = player.GetComponent<IdleAutoplay>();
            if (idleScript != null)
                idleScript.enabled = false;

            // Enable CarControllerRB
            CarControllerRB controller = player.GetComponent<CarControllerRB>();
            if (controller != null)
                controller.enabled = true;

            // Freeze Rigidbody2D
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // Traffic light sequence
        redLight.SetActive(true);
        yield return new WaitForSeconds(1f);
        redLight.SetActive(false);

        redLight2.SetActive(true);
        yield return new WaitForSeconds(1f);
        redLight2.SetActive(false);

        redLight3.SetActive(true);
        yield return new WaitForSeconds(1f);
        redLight3.SetActive(false);

        greenLight.SetActive(true);
        yield return new WaitForSeconds(1f);
        greenLight.SetActive(false);

        // Unfreeze Rigidbody2D and enable visibility checks
        foreach (GameObject player in players)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.constraints = RigidbodyConstraints2D.None;

            PlayerVisibilityCheck visibility = player.GetComponent<PlayerVisibilityCheck>();
            if (visibility != null)
                visibility.EnableCheck();
        }

    }
}
