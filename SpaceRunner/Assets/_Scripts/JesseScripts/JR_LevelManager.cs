using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JR_LevelManager : MonoBehaviour
{
    //Find the level script to determine what level your in
    private JR_Level m_Level;

    // Start is called before the first frame update
    void Start()
    {
        //Set the varavle to the script in your scene
        m_Level = GameObject.FindObjectOfType<JR_Level>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            CheckLevel(); 
        }
    }
    //Check which scene will be next
    public void CheckLevel()
    {
        //Determine if in level one
        if(m_Level.Level == 1)
        {
            SceneManager.LoadScene("Level_2");
        }
        //Determine if in level two
        else if (m_Level.Level == 2)
        {
            SceneManager.LoadScene("Level_3");
        }
        else if (m_Level.Level == 3)
        {
            SceneManager.LoadScene("Level_4");
        }
        //Determine if in level three
        else if (m_Level.Level == 4)
        {
            SceneManager.LoadScene("BossFight");
        }
        //Determine if in level four
        else if (m_Level.Level == 5)
        {
            SceneManager.LoadScene("Credits");
        }
        //Determine if in level five
        else if (m_Level.Level == 6)
        {
            SceneManager.LoadScene("StartMenu");
        }
    }

    public void RestartLevel()
    {
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex); 
    }
}
