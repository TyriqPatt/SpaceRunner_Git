using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JR_WinSceneManager : MonoBehaviour
{
    public Button m_nextButton;
    public Button m_quitButton;
    private JR_RestLevel m_resetLevel;
    // Start is called before the first frame update
    void Start()
    {
        m_resetLevel = GameObject.FindObjectOfType<JR_RestLevel>();
        m_nextButton.onClick.AddListener(m_nextButton_onClick);
        m_quitButton.onClick.AddListener(m_quitButton_onClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void m_nextButton_onClick()
    {
        m_resetLevel.GoToNextLevel(); 
    }

    void m_quitButton_onClick()
    {
        Application.Quit();
    }
}
