using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JR_StartMenu : MonoBehaviour
{

    //public GameObject buttonStart;
    public GameObject buttonYesNo;
    public GameObject buttonSettings;

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
        buttonSettings.SetActive(false);

    }

    public void PlayTutorial()
    {
      
        SceneManager.LoadScene("SpaceRunner");
    }
    public void SkipTutorial()
    {
       
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGame()
    {
        buttonSettings.SetActive(false);
        buttonYesNo.SetActive(false);
        Application.Quit();
    }

    public void OpenSettings()
    {
        buttonYesNo.SetActive(false);
        buttonSettings.SetActive(true);

    }

    public void BackButton()
    {
        buttonSettings.SetActive(false);
        buttonYesNo.SetActive(false);
    }
}
