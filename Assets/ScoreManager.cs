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

    private void Start()
    {
        //just setting the highscore text here cause i'm lazy :P
        if (HighscoreText == null)
        {
            HighscoreText = GameObject.Find("T_Score")?.GetComponent<TMP_Text>();
        }

        //finding all player text gameobjects and adding them to an array
        for (int i = 0; i < playerScores.Length; i++)
        {
            //why tf are you not working, i think i'm actually stupid
            //i just want to add gameobjects to a list maaaaaaaaaaaaaaaaaaaaaaaaaan
            GameObject textObj = GameObject.Find("T_Count_" + i);
            PlayerScoreText.Add(textObj);

            Debug.Log("T_Count_" + i);
            Debug.Log(textObj);
        }
    }

    void Update()
    {
        if (Time.time > nextActionTime)
        {
            //every second the player is alive it adds 1 to that player's score
            nextActionTime += WaitTime;
            for (int i = 0; i < playerScores.Length; i++)
            {
                playerScores[i] += 1;
                //finds the player text of this current player whose score we added to and displays the score
                if (PlayerScoreText[i] != null)
                {
                    PlayerScoreText[i].GetComponent<TMP_Text>().text = playerScores[i].ToString();
                }
            }

            //execute code here

            //goes through all scores in array, if it's bigger than the high score then it sets it replaces it
            foreach (int score in playerScores)
            {
                if (score > highscore)
                {
                    highscore = score;
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
}
