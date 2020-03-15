using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class JR_LoseConditions : MonoBehaviour
{
    PlayerHealth m_playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        m_playerHealth = GameObject.FindObjectOfType<PlayerHealth>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerHealth.CurrentHealth <= 0)
        {
            StartCoroutine(Delay()); 
        }
        
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LoseScene");

    }
}
