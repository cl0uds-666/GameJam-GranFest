using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject yellowLight;
    public GameObject greenLight;

    private bool gameStarted = false;

    void Start()
    {
        tapToPlayCanvas.SetActive(true);
        mainUICanvas.SetActive(false);

        redLight.SetActive(false);
        yellowLight.SetActive(false);
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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        if (!gameStarted && Input.GetMouseButtonDown(0))
=======
=======
>>>>>>> Stashed changes
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!gameStarted)
>>>>>>> Stashed changes
        {
            StartCoroutine(BeginGameSequence());
        }
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
        GameObject.Find("AudioManager").GetComponent<AudioManager>().SFXSource.PlayOneShot(GameObject.Find("AudioManager").GetComponent<AudioManager>().IdleStartNoise);
        yield return new WaitForSeconds(1f);
        redLight.SetActive(false);

<<<<<<< Updated upstream
        yellowLight.SetActive(true);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().SFXSource.PlayOneShot(GameObject.Find("AudioManager").GetComponent<AudioManager>().IdleStartNoise);
        yield return new WaitForSeconds(1f);
        yellowLight.SetActive(false);
=======
        redLight2.SetActive(true);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().SFXSource.PlayOneShot(GameObject.Find("AudioManager").GetComponent<AudioManager>().IdleStartNoise);
        yield return new WaitForSeconds(1f);
        redLight2.SetActive(false);

        redLight3.SetActive(true);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().SFXSource.PlayOneShot(GameObject.Find("AudioManager").GetComponent<AudioManager>().IdleStartNoise);
        yield return new WaitForSeconds(1f);
        redLight3.SetActive(false);
>>>>>>> Stashed changes

        greenLight.SetActive(true);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().SFXSource.PlayOneShot(GameObject.Find("AudioManager").GetComponent<AudioManager>().IdleStartNoise);
        yield return new WaitForSeconds(1f);
        greenLight.SetActive(false);

        // Unfreeze Rigidbody2D
        foreach (GameObject player in players)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.constraints = RigidbodyConstraints2D.None;
        }
    }
}
