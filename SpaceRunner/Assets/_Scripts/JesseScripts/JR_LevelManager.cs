using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JR_LevelManager : MonoBehaviour
{
    //Find the level script to determine what level your in
    private JR_Level m_Level;
    public int CurrentLevel; 

    // Start is called before the first frame update
    void Start()
    {
        //Set the varavle to the script in your scene
    }

    // Update is called once per frame
    void Update()
    {
        m_Level = GameObject.FindObjectOfType<JR_Level>();

        if (m_Level != null)
        {
            CurrentLevel = m_Level.Level; 
        }
        else
        {
            Debug.Log("No Level Script");
        }
    }
    //Check which scene will be next
   

    public void GoToNextLevel()
    {
        if (CurrentLevel == -1)
        {
            SceneManager.LoadScene("SpaceRunner");
        }
        //Determine if in level one
        else if (CurrentLevel == 0)
        {
            SceneManager.LoadScene("Level_1");
        }
        //Determine if in level two
        else if (CurrentLevel == 1)
        {
            SceneManager.LoadScene("Level_2");
        }
        else if (m_Level.Level == 2)
        {
            SceneManager.LoadScene("Level_3");
        }
        //Determine if in level three
        else if (CurrentLevel == 3)
        {
            SceneManager.LoadScene("Level_4");
        }
        //Determine if in level four
        else if (CurrentLevel == 4)
        {
            SceneManager.LoadScene("BossFight");
        }
        //Determine if in level five
        else if (CurrentLevel == 5)
        {
            SceneManager.LoadScene("Credits");
        }
       
    }

    public void RetryLevel()
    {
        if (CurrentLevel == -1)
        {
            SceneManager.LoadScene("StartMenu");
        }
        //Determine if in level one
        else if (CurrentLevel == 0)
        {
            SceneManager.LoadScene("SpaceRunner");
        }
        //Determine if in level two
        else if (CurrentLevel == 1)
        {
            SceneManager.LoadScene("Level_1");
        }
        else if (CurrentLevel == 2)
        {
            SceneManager.LoadScene("Level_2");
        }
        //Determine if in level three
        else if (CurrentLevel == 3)
        {
            SceneManager.LoadScene("Level_3");
        }
        //Determine if in level four
        else if (CurrentLevel == 4)
        {
            SceneManager.LoadScene("Level_4");
        }
        //Determine if in level five
        else if (CurrentLevel == 5)
        {
            SceneManager.LoadScene("BossFight");
        }
        else if (CurrentLevel == 6)
        {
            SceneManager.LoadScene("Credits");
        }

    }


}
