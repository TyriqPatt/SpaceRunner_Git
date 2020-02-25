using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JR_LoseSceneManager : MonoBehaviour
{
    public Button m_retryButton;
    public Button m_quitButton;
    private JR_RestLevel m_resetLevel;
    // Start is called before the first frame update
    void Start()
    {
        m_resetLevel = GameObject.FindObjectOfType<JR_RestLevel>();
        m_retryButton.onClick.AddListener(m_retryButton_onClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void m_retryButton_onClick()
    {
        m_resetLevel.ResetLevel(); 
    }
}
