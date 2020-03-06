using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JR_StartMenu : MonoBehaviour
{

    //public GameObject buttonStart;
    public GameObject buttonYesNo;

    //public GameObject DestroyObject;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartGame()
    {
       // buttonStart.SetActive(false);
        buttonYesNo.SetActive(true);  

    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("SpaceRunner");
    }
    public void SkipTutorial()
    {
        SceneManager.LoadScene("Level_1");

    }
}
