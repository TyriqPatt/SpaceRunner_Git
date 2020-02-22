using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JR_PauseMenu : MonoBehaviour
{
    //The pause canvas
    public GameObject PauseMenu;
    //Check if it is paused or not
    private bool isPaused; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //pause input key
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //Check if not paused
            if (!isPaused)
            {
                //Pause screen function
                PauseGame();
            }
            //Check if is paused
            else
            {
                //Removes pause screen function
                UnpauseGame(); 
            }
        }
        
    }

    //Call pause dscreen function
    private void PauseGame()
    {
        //Set the pause canvas true
        PauseMenu.SetActive(true);
        //To create the switch on and off feature
        isPaused = true;
    }
    //Remove pause screen
    private void UnpauseGame()
    {
        //Set the pause canvas as false 
        PauseMenu.SetActive(false);
        //To create the switch on and off feature
        isPaused = false;
    }
}
