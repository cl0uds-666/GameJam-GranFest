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
        HighscoreText = GameObject.Find("T_Score").GetComponent<TMP_Text>();

        //finding all player text gameobjects and adding them to an array
        foreach (int playerIndx in playerScores)
        {
            //why tf are you not working, i think i'm actually stupid
            //i just want to add gameobjects to a list maaaaaaaaaaaaaaaaaaaaaaaaaan
            PlayerScoreText.Add(GameObject.Find("T_Count_" + playerIndx));

            Debug.Log(("T_Count_" + playerIndx));
            Debug.Log(GameObject.Find("T_Count_" + playerIndx));
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
                PlayerScoreText[i - 1].GetComponent<TMP_Text>().text = playerScores[i - 1].ToString();
                
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
            HighscoreText.text = highscore.ToString();
        }
    }

    //resets scoreee
    public void PlayerReset(int PlayerNum)
    {
        playerScores[PlayerNum] = 0;
    }
}
