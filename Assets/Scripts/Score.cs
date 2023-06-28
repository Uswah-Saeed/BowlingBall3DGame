using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Score
{
    private static Score instance;
    private int score = 0;

    private Score() { }
    public static Score GetInstance()
    {
        if (instance == null)
        {
            instance = new Score();
        }
        return instance;
    }
    public void AddScore(int point)
    {
        score = score + point;

        Debug.Log(this.score + "score");
        if (GameObject.FindWithTag("Score") != null)
        {
            GameObject.FindWithTag("Score").GetComponent<Text>().text = "Score " + score;
        }
    }
   
    public int GetScore()
    {
        return score;
    }
    public void reset_scoreboard()
    {
        score = 0;
    }
}
