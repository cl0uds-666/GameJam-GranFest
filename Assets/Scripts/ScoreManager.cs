using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text HighscoreText;

    private float nextActionTime = 0.0f;
    public float WaitTime = 1f;
    public int[] playerScores;
    public List<GameObject> PlayerScoreText = new List<GameObject>();
    public int highscore;
    public int currentHighscorePlayer = 0; // track which player is currently leading

    private void Start()
    {
        //just setting the highscore text here cause i'm lazy :P
        if (HighscoreText == null)
        {
            HighscoreText = GameObject.Find("T_Score")?.GetComponent<TMP_Text>();
        }

        // Load highscore from PlayerPrefs
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        if (HighscoreText != null)
        {
            HighscoreText.text = highscore.ToString();
        }

        //finding all player text gameobjects and adding them to an array
        for (int i = 0; i < playerScores.Length; i++)
        {
            GameObject textObj = GameObject.Find("T_Count_" + i);
            PlayerScoreText.Add(textObj);
            //why tf are you not working, i think i'm actually stupid
            //i just want to add gameobjects to a list maaaaaaaaaaaaaaaaaaaaaaaaaan
            Debug.Log("T_Count_" + i);
            Debug.Log(textObj);
        }
    }




    void Update()
    {
        if (Time.time > nextActionTime)
        {
            // every second the player is alive it adds 1 to that player's score
            nextActionTime += WaitTime;
            for (int i = 0; i < playerScores.Length; i++)
            {
                playerScores[i] += 1;
                // finds the player text of this current player whose score we added to and displays the score
                if (PlayerScoreText[i] != null)
                {
                    PlayerScoreText[i].GetComponent<TMP_Text>().text = playerScores[i].ToString();
                }
            }

            // goes through all scores in array, if it's bigger than the high score then it replaces it
            for (int i = 0; i < playerScores.Length; i++)
            {
                if (playerScores[i] > highscore)
                {
                    highscore = playerScores[i];
                    currentHighscorePlayer = i; // update current highscore player

                    // Save new highscore to PlayerPrefs
                    PlayerPrefs.SetInt("Highscore", highscore);
                    PlayerPrefs.Save();
                }
            }

            if (HighscoreText != null)
            {
                HighscoreText.text = highscore.ToString();
            }
        }
    }


    //resets scoreee
    public void PlayerReset(int PlayerNum)
    {
        if (PlayerNum >= 0 && PlayerNum < playerScores.Length)
        {
            playerScores[PlayerNum] = 0;
        }
    }

    // deducts points from a specific player
    public void DeductScore(int playerIndex, int amount)
    {
        if (playerIndex >= 0 && playerIndex < playerScores.Length)
        {
            playerScores[playerIndex] = Mathf.Max(0, playerScores[playerIndex] - amount);
            if (PlayerScoreText[playerIndex] != null)
            {
                PlayerScoreText[playerIndex].GetComponent<TMP_Text>().text = playerScores[playerIndex].ToString();
            }
        }
    }


    // returns the player GameObject currently leading the score
    public GameObject GetHighscorePlayer()
    {
        return GameObject.Find("Player_" + currentHighscorePlayer);
    }
}
