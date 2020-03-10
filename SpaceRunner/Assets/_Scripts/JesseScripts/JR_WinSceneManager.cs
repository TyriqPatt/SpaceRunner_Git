using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JR_WinSceneManager : MonoBehaviour
{
    public Button m_nextButton;
    public Button m_quitButton;
    public Button m_retryButton;
    private JR_LevelManager m_levelManager;
    // Start is called before the first frame update
    void Start()
    {
        m_levelManager = GameObject.FindObjectOfType<JR_LevelManager>();
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
        m_levelManager.GoToNextLevel(); 
    }

    void m_quitButton_onClick()
    {
        Application.Quit();
    }
    void m_retryButton_OnClick()
    {
        m_levelManager.RetryLevel(); 
    }



}
