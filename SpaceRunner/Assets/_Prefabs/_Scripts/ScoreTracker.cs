using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{

    public Text HighScore;
    public Text Score;
    public int CurScore;
    

    // Start is called before the first frame update
    void Start()
    {
        HighScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FinalScore()
    {
        
        
        
    }

    public void Addpoints(int points)
    {
        CurScore += points;
        Score.text = CurScore.ToString();

        if (CurScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", CurScore);
            HighScore.text = CurScore.ToString();
        }
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
    }
}
