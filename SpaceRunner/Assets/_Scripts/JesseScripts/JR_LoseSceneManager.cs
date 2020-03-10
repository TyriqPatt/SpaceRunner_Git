using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JR_LoseSceneManager : MonoBehaviour
{
    public Button m_retryButton;
    public Button m_quitButton;
    private JR_LevelManager m_levelManager;
    // Start is called before the first frame update
    void Start()
    {
        m_levelManager = GameObject.FindObjectOfType<JR_LevelManager>();
        m_retryButton.onClick.AddListener(m_retryButton_onClick);
        m_quitButton.onClick.AddListener(m_quitButton_onClick);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void m_retryButton_onClick()
    {
        RetryLevel();  
    }

    void m_quitButton_onClick()
    {
        Application.Quit();
    }

    

    public void RetryLevel()
    {
        if (m_levelManager.CurrentLevel == -1)
        {
            SceneManager.LoadScene("StartMenu");
        }
        //Determine if in level one
        if (m_levelManager.CurrentLevel == 0)
        {
            SceneManager.LoadScene("SpaceRunner");
        }
        //Determine if in level two
        if (m_levelManager.CurrentLevel == 1)
        {
            SceneManager.LoadScene("Level_1");
        }
        if (m_levelManager.CurrentLevel == 2)
        {
            SceneManager.LoadScene("Level_2");
        }
        //Determine if in level three
        if (m_levelManager.CurrentLevel == 3)
        {
            SceneManager.LoadScene("Level_3");
        }
        //Determine if in level four
        if (m_levelManager.CurrentLevel == 4)
        {
            SceneManager.LoadScene("Level_4");
        }
        //Determine if in level five
        if (m_levelManager.CurrentLevel == 5)
        {
            SceneManager.LoadScene("BossFight");
        }
        if (m_levelManager.CurrentLevel == 6)
        {
            SceneManager.LoadScene("Credits");
        }

    }



}
