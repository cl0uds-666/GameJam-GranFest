using UnityEngine;

public class PlayerVisibilityCheck : MonoBehaviour
{
    private Camera mainCam;
    private Renderer rend;
    private CarControllerRB controller;
    private int playerIndex;

    private AudioManager AudioManager;

    private void Start()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        mainCam = Camera.main;
        rend = GetComponent<Renderer>();
        controller = GetComponent<CarControllerRB>();

        // Disable this script until the game starts
        enabled = false;

        // Extract player index from name
        string[] splitName = gameObject.name.Split('_');
        if (splitName.Length > 1 && int.TryParse(splitName[1], out int index))
        {
            playerIndex = index;
        }
        else
        {
            Debug.LogError("Couldn't determine player index from name!");
        }
    }

    private void Update()
    {
        if (!IsVisible())
        {
            DieAndRespawn();
        }
    }

    public void EnableCheck()
    {
        enabled = true;
    }

    private bool IsVisible()
    {
        if (rend == null) return true;

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCam);
        return GeometryUtility.TestPlanesAABB(planes, rend.bounds);
    }

    private void DieAndRespawn()
    {
        ScoreManager sm = FindFirstObjectByType<ScoreManager>();
        if (sm != null)
        {
            //playing explosing sound
            AudioManager.SFXSource.PlayOneShot(AudioManager.Explode);

            sm.PlayerReset(playerIndex);
        }

        // Get world position near bottom of screen
        Vector3 screenPos = new Vector3(Screen.width / 2, Screen.height * 0.2f, 0f);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;

        // Offset by index so players don't overlap
        worldPos += new Vector3(playerIndex * 1.5f, 0f, 0f);

        // Default rotation fallback
        Quaternion newRotation = Quaternion.identity;

        // Get lead player's rotation
        if (sm != null && sm.playerScores.Length > 0)
        {
            int leadIndex = 0;
            int highestScore = sm.playerScores[0];

            for (int i = 1; i < sm.playerScores.Length; i++)
            {
                if (sm.playerScores[i] > highestScore)
                {
                    highestScore = sm.playerScores[i];
                    leadIndex = i;
                }
            }

            GameObject leadPlayer = GameObject.Find("Player_" + leadIndex);
            if (leadPlayer != null)
            {
                newRotation = leadPlayer.transform.rotation;
            }
        }

        // Apply position and rotation
        transform.position = worldPos;
        transform.rotation = newRotation;

        controller.SetControlEnabled(false);
        Invoke(nameof(EnableControl), 1f);
    }



    private void EnableControl()
    {
        controller.SetControlEnabled(true);
    }
}
