using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JR_Level : MonoBehaviour
{
    //This script is just to assign which level this is - level one, level two, etc. 
    public int Level;
    JR_LevelManager m_levelManager;
    // Start is called before the first frame update
    void Start()
    {
        m_levelManager = GameObject.FindObjectOfType<JR_LevelManager>();
        m_levelManager.CurrentLevel = Level;  
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
