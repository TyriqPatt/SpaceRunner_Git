﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class JR_RestLevel : MonoBehaviour
{
    public int currentLevel;
   

    JR_Level m_Level;
    public string CurrentLevelName = "";
    //public string NextLevelName = "";

    // Start is called before the first frame update
    void Start()
    {
        m_Level = GameObject.FindObjectOfType<JR_Level>();
        if(m_Level == null)
        {
            return;
        }
        currentLevel = m_Level.Level;
        DetermineLevel();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            GoToNextLevel(); 
        }
    }


   

    public void ResetLevel()
    {
        currentLevel -= 1;
        SceneManager.LoadScene(CurrentLevelName);
    }

    public void GoToNextLevel()
    {
       
        currentLevel += 1;
        DetermineLevel();
        SceneManager.LoadScene(CurrentLevelName);

    }

    void DetermineLevel()
    {
        if(currentLevel == 1)
        {
            CurrentLevelName = "Level_1";
        }
        else if (currentLevel == 2)
        {
            CurrentLevelName = "Level_2";
        }
        else if (currentLevel == 3)
        {
            CurrentLevelName = "Level_3";
        }
        else if (currentLevel == 4)
        {
            CurrentLevelName = "Level_4";
        }
        else if (currentLevel == 5)
        {
            CurrentLevelName = "BossFight";
        }
        else if (currentLevel == 0)
        {
            CurrentLevelName = "Level_1";
        }


    }

   /* void DetermineNextLevel()
    {
        if (currentLevel == 1)
        {
            NextLevelName = "Level_2";
        }
        else if (currentLevel == 2)
        {
            NextLevelName = "Level_3";
        }
        else if (currentLevel == 3)
        {
            NextLevelName = "Level_4";
        }
        else if (currentLevel == 4)
        {
            NextLevelName = "BossFight";
        }
       
    }*/ 

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
