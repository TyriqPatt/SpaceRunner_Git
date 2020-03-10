using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JR_LoseSceneManager : MonoBehaviour
{
    public Button m_retryButton;
    public Button m_quitButton;
    private JR_LevelManager m_levelManger;
    // Start is called before the first frame update
    void Start()
    {
        m_levelManger = GameObject.FindObjectOfType<JR_LevelManager>();
        m_retryButton.onClick.AddListener(m_retryButton_onClick);
        m_quitButton.onClick.AddListener(m_quitButton_onClick);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void m_retryButton_onClick()
    {
        m_levelManger.RetryLevel();  
    }

    void m_quitButton_onClick()
    {
        Application.Quit();
    }
}
