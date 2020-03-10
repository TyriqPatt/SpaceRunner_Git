using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JR_WinSceneManager : MonoBehaviour
{
    public Button m_nextButton;
    public Button m_quitButton;
    public Button m_retryButton;
    private JR_LevelManager m_levelManager;

    public int whichLevel; 
    // Start is called before the first frame update
    void Start()
    {
        m_levelManager = GameObject.FindObjectOfType<JR_LevelManager>();
        whichLevel = m_levelManager.CurrentLevel; 
        m_nextButton.onClick.AddListener(m_nextButton_onClick);
        m_quitButton.onClick.AddListener(m_quitButton_onClick);
        m_retryButton.onClick.AddListener(m_retryButton_OnClick);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void m_nextButton_onClick()
    {
        GoToNextLevel(); 
    }

    void m_quitButton_onClick()
    {
        Application.Quit();
    }
    void m_retryButton_OnClick()
    {
        RetryLevel(); 
    }


    public void GoToNextLevel()
    {
        if (whichLevel == -1)
        {
            SceneManager.LoadScene("SpaceRunner");
        }
        //Determine if in level one
        if (whichLevel == 0)
        {
            SceneManager.LoadScene("Level_1");
        }
        //Determine if in level two
        if (whichLevel == 1)
        {
            SceneManager.LoadScene("Level_2");
        }
        if (whichLevel == 2)
        {
            SceneManager.LoadScene("Level_3");
        }
        //Determine if in level three
        if (whichLevel == 3)
        {
            SceneManager.LoadScene("Level_4");
        }
        //Determine if in level four
        if (whichLevel == 4)
        {
            SceneManager.LoadScene("BossFight");
        }
        //Determine if in level five
        if (whichLevel == 5)
        {
            SceneManager.LoadScene("Credits");
        }

    }

    public void RetryLevel()
    {
        if (whichLevel == -1)
        {
            SceneManager.LoadScene("StartMenu");
        }
        //Determine if in level one
        if (whichLevel == 0)
        {
            SceneManager.LoadScene("SpaceRunner");
        }
        //Determine if in level two
        if (whichLevel == 1)
        {
            SceneManager.LoadScene("Level_1");
        }
        if (whichLevel == 2)
        {
            SceneManager.LoadScene("Level_2");
        }
        //Determine if in level three
        if (whichLevel == 3)
        {
            SceneManager.LoadScene("Level_3");
        }
        //Determine if in level four
        if (whichLevel == 4)
        {
            SceneManager.LoadScene("Level_4");
        }
        //Determine if in level five
        if (whichLevel == 5)
        {
            SceneManager.LoadScene("BossFight");
        }
        if (whichLevel == 6)
        {
            SceneManager.LoadScene("Credits");
        }

    }





}
